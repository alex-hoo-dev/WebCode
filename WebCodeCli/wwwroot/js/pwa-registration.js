/**
 * PWA Service Worker 注册脚本
 * 负责注册、更新和管理Service Worker
 */

(function() {
  'use strict';

  const PWA = {
    // 配置
    config: {
      serviceWorkerPath: '/service-worker.js',
      updateCheckInterval: 60 * 60 * 1000, // 每小时检查一次更新
      promptUpdateDelay: 5000 // 更新提示延迟显示
    },

    // 状态
    state: {
      registration: null,
      updateAvailable: false,
      isInstalled: false,
      deferredPrompt: null
    },

    // 初始化
    init: function() {
      if (!('serviceWorker' in navigator)) {
        console.log('[PWA] Service Worker 不受支持');
        return;
      }

      // 检测是否已安装为PWA
      this.checkInstallState();

      // 注册Service Worker
      this.registerServiceWorker();

      // 监听安装提示事件
      this.listenForInstallPrompt();

      // 监听安装状态变化
      this.listenForAppInstalled();

      console.log('[PWA] 初始化完成');
    },

    // 检测安装状态
    checkInstallState: function() {
      // 检测是否以独立模式运行（已安装）
      if (window.matchMedia('(display-mode: standalone)').matches) {
        this.state.isInstalled = true;
        document.body.classList.add('pwa-installed');
        console.log('[PWA] 应用已安装并以独立模式运行');
      }

      // iOS Safari 检测
      if (window.navigator.standalone === true) {
        this.state.isInstalled = true;
        document.body.classList.add('pwa-installed', 'ios-standalone');
        console.log('[PWA] 应用已添加到iOS主屏幕');
      }
    },

    // 注册Service Worker
    registerServiceWorker: async function() {
      try {
        const registration = await navigator.serviceWorker.register(
          this.config.serviceWorkerPath,
          { scope: '/' }
        );

        this.state.registration = registration;
        console.log('[PWA] Service Worker 注册成功，作用域:', registration.scope);

        // 监听更新
        registration.addEventListener('updatefound', () => {
          this.onUpdateFound(registration);
        });

        // 检查是否有等待中的worker
        if (registration.waiting) {
          this.onUpdateReady(registration.waiting);
        }

        // 定期检查更新
        setInterval(() => {
          registration.update();
        }, this.config.updateCheckInterval);

        // 监听控制器变化（新Service Worker已接管）
        navigator.serviceWorker.addEventListener('controllerchange', () => {
          console.log('[PWA] Service Worker 已更新，页面将刷新');
          // 可以在这里提示用户或自动刷新
        });

      } catch (error) {
        console.error('[PWA] Service Worker 注册失败:', error);
      }
    },

    // 发现更新
    onUpdateFound: function(registration) {
      const newWorker = registration.installing;
      console.log('[PWA] 发现新版本，正在安装...');

      newWorker.addEventListener('statechange', () => {
        if (newWorker.state === 'installed' && navigator.serviceWorker.controller) {
          this.onUpdateReady(newWorker);
        }
      });
    },

    // 更新准备就绪
    onUpdateReady: function(worker) {
      this.state.updateAvailable = true;
      console.log('[PWA] 新版本已准备就绪');

      // 延迟显示更新提示
      setTimeout(() => {
        this.showUpdateNotification(worker);
      }, this.config.promptUpdateDelay);
    },

    // 显示更新通知
    showUpdateNotification: function(worker) {
      // 创建更新提示UI
      const notification = document.createElement('div');
      notification.className = 'pwa-update-notification';
      notification.innerHTML = `
        <div class="pwa-update-content">
          <div class="pwa-update-icon">
            <svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
              <path d="M23 4v6h-6M1 20v-6h6"></path>
              <path d="M3.51 9a9 9 0 0114.85-3.36L23 10M1 14l4.64 4.36A9 9 0 0020.49 15"></path>
            </svg>
          </div>
          <div class="pwa-update-text">
            <strong>发现新版本</strong>
            <span>点击更新以获取最新功能</span>
          </div>
          <button class="pwa-update-btn" id="pwa-update-btn">立即更新</button>
          <button class="pwa-update-close" id="pwa-update-close">&times;</button>
        </div>
      `;

      // 添加样式
      if (!document.getElementById('pwa-update-styles')) {
        const styles = document.createElement('style');
        styles.id = 'pwa-update-styles';
        styles.textContent = `
          .pwa-update-notification {
            position: fixed;
            bottom: 80px;
            left: 16px;
            right: 16px;
            max-width: 400px;
            margin: 0 auto;
            background: #1e293b;
            color: white;
            border-radius: 12px;
            box-shadow: 0 10px 40px rgba(0,0,0,0.3);
            z-index: 10000;
            animation: pwa-slide-up 0.3s ease-out;
            padding-bottom: env(safe-area-inset-bottom, 0);
          }
          .pwa-update-content {
            display: flex;
            align-items: center;
            gap: 12px;
            padding: 16px;
          }
          .pwa-update-icon {
            flex-shrink: 0;
            width: 40px;
            height: 40px;
            background: #3b82f6;
            border-radius: 10px;
            display: flex;
            align-items: center;
            justify-content: center;
          }
          .pwa-update-icon svg {
            color: white;
          }
          .pwa-update-text {
            flex: 1;
            display: flex;
            flex-direction: column;
            gap: 2px;
          }
          .pwa-update-text strong {
            font-size: 14px;
          }
          .pwa-update-text span {
            font-size: 12px;
            opacity: 0.8;
          }
          .pwa-update-btn {
            flex-shrink: 0;
            padding: 8px 16px;
            background: #3b82f6;
            color: white;
            border: none;
            border-radius: 8px;
            font-size: 14px;
            font-weight: 500;
            cursor: pointer;
            transition: background 0.2s;
          }
          .pwa-update-btn:hover {
            background: #2563eb;
          }
          .pwa-update-btn:active {
            transform: scale(0.98);
          }
          .pwa-update-close {
            position: absolute;
            top: 8px;
            right: 8px;
            width: 24px;
            height: 24px;
            background: transparent;
            border: none;
            color: rgba(255,255,255,0.6);
            font-size: 20px;
            cursor: pointer;
            display: flex;
            align-items: center;
            justify-content: center;
          }
          .pwa-update-close:hover {
            color: white;
          }
          @keyframes pwa-slide-up {
            from {
              opacity: 0;
              transform: translateY(20px);
            }
            to {
              opacity: 1;
              transform: translateY(0);
            }
          }
        `;
        document.head.appendChild(styles);
      }

      document.body.appendChild(notification);

      // 更新按钮点击事件
      document.getElementById('pwa-update-btn').addEventListener('click', () => {
        worker.postMessage({ type: 'SKIP_WAITING' });
        notification.remove();
        window.location.reload();
      });

      // 关闭按钮点击事件
      document.getElementById('pwa-update-close').addEventListener('click', () => {
        notification.remove();
      });
    },

    // 监听安装提示事件
    listenForInstallPrompt: function() {
      window.addEventListener('beforeinstallprompt', (e) => {
        // 阻止默认安装提示
        e.preventDefault();
        this.state.deferredPrompt = e;
        console.log('[PWA] 安装提示已准备就绪');

        // 可以在这里显示自定义安装按钮
        this.showInstallButton();
      });
    },

    // 显示安装按钮
    showInstallButton: function() {
      // 如果已安装，不显示
      if (this.state.isInstalled) return;

      // 添加安装按钮到设置页面
      const installBtn = document.createElement('div');
      installBtn.id = 'pwa-install-prompt';
      installBtn.className = 'pwa-install-prompt';
      installBtn.innerHTML = `
        <div class="pwa-install-content">
          <div class="pwa-install-icon">
            <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
              <path d="M21 15v4a2 2 0 01-2 2H5a2 2 0 01-2-2v-4M7 10l5 5 5-5M12 15V3"></path>
            </svg>
          </div>
          <div class="pwa-install-text">
            <strong>添加到主屏幕</strong>
            <span>快速访问，离线可用</span>
          </div>
          <button class="pwa-install-btn">安装</button>
        </div>
      `;

      // 添加样式
      if (!document.getElementById('pwa-install-styles')) {
        const styles = document.createElement('style');
        styles.id = 'pwa-install-styles';
        styles.textContent = `
          .pwa-install-prompt {
            margin: 16px;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
            border-radius: 16px;
            overflow: hidden;
            animation: pwa-fade-in 0.3s ease-out;
          }
          .pwa-install-content {
            display: flex;
            align-items: center;
            gap: 12px;
            padding: 16px;
          }
          .pwa-install-icon {
            flex-shrink: 0;
            width: 44px;
            height: 44px;
            background: rgba(255,255,255,0.2);
            border-radius: 12px;
            display: flex;
            align-items: center;
            justify-content: center;
          }
          .pwa-install-text {
            flex: 1;
            display: flex;
            flex-direction: column;
            gap: 2px;
          }
          .pwa-install-text strong {
            font-size: 15px;
          }
          .pwa-install-text span {
            font-size: 13px;
            opacity: 0.9;
          }
          .pwa-install-btn {
            flex-shrink: 0;
            padding: 10px 20px;
            background: white;
            color: #764ba2;
            border: none;
            border-radius: 10px;
            font-size: 14px;
            font-weight: 600;
            cursor: pointer;
            transition: transform 0.2s;
          }
          .pwa-install-btn:active {
            transform: scale(0.95);
          }
          @keyframes pwa-fade-in {
            from { opacity: 0; transform: scale(0.95); }
            to { opacity: 1; transform: scale(1); }
          }
        `;
        document.head.appendChild(styles);
      }

      // 监听DOM变化，将安装提示添加到设置页面
      const observer = new MutationObserver(() => {
        const settingsContainer = document.querySelector('.h-full.overflow-y-auto.bg-gray-50');
        if (settingsContainer && !document.getElementById('pwa-install-prompt')) {
          const firstChild = settingsContainer.querySelector('.max-w-lg');
          if (firstChild) {
            firstChild.insertBefore(installBtn, firstChild.firstChild);
            
            // 绑定点击事件
            installBtn.querySelector('.pwa-install-btn').addEventListener('click', () => {
              this.promptInstall();
            });
          }
        }
      });

      observer.observe(document.body, { childList: true, subtree: true });
    },

    // 提示安装
    promptInstall: async function() {
      if (!this.state.deferredPrompt) {
        console.log('[PWA] 无法触发安装提示');
        return;
      }

      // 显示安装提示
      this.state.deferredPrompt.prompt();

      // 等待用户响应
      const { outcome } = await this.state.deferredPrompt.userChoice;
      console.log('[PWA] 用户选择:', outcome);

      // 清除保存的提示
      this.state.deferredPrompt = null;

      // 隐藏安装按钮
      const installPrompt = document.getElementById('pwa-install-prompt');
      if (installPrompt) {
        installPrompt.remove();
      }
    },

    // 监听应用已安装事件
    listenForAppInstalled: function() {
      window.addEventListener('appinstalled', () => {
        this.state.isInstalled = true;
        this.state.deferredPrompt = null;
        document.body.classList.add('pwa-installed');
        console.log('[PWA] 应用已成功安装');

        // 隐藏安装提示
        const installPrompt = document.getElementById('pwa-install-prompt');
        if (installPrompt) {
          installPrompt.remove();
        }
      });
    },

    // 获取Service Worker版本
    getVersion: async function() {
      if (!this.state.registration || !this.state.registration.active) {
        return null;
      }

      return new Promise((resolve) => {
        const channel = new MessageChannel();
        channel.port1.onmessage = (event) => {
          resolve(event.data.version);
        };
        this.state.registration.active.postMessage(
          { type: 'GET_VERSION' },
          [channel.port2]
        );
      });
    }
  };

  // DOM加载完成后初始化
  if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', () => PWA.init());
  } else {
    PWA.init();
  }

  // 导出到全局
  window.PWA = PWA;

})();
