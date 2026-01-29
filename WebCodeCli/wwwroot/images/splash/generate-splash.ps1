# PWA 启动画面生成脚本
# 需要安装 ImageMagick: winget install ImageMagick.ImageMagick

$splashScreens = @(
    @{ width = 640; height = 1136; name = "splash-640x1136.png" },   # iPhone 5
    @{ width = 750; height = 1334; name = "splash-750x1334.png" },   # iPhone 6/7/8
    @{ width = 1242; height = 2208; name = "splash-1242x2208.png" }, # iPhone 6+/7+/8+
    @{ width = 1125; height = 2436; name = "splash-1125x2436.png" }, # iPhone X/XS
    @{ width = 1242; height = 2688; name = "splash-1242x2688.png" }, # iPhone XS Max
    @{ width = 828; height = 1792; name = "splash-828x1792.png" }    # iPhone XR
)

$logoPath = Join-Path $PSScriptRoot "..\logo.png"
$outputDir = $PSScriptRoot
$backgroundColor = "#2563eb"

Write-Host "PWA 启动画面生成脚本" -ForegroundColor Cyan
Write-Host ""

# 检查 ImageMagick 是否安装
$magick = Get-Command magick -ErrorAction SilentlyContinue
if (-not $magick) {
    Write-Host "未检测到 ImageMagick，请先安装:" -ForegroundColor Yellow
    Write-Host "  winget install ImageMagick.ImageMagick" -ForegroundColor Green
    exit 1
}

Write-Host "开始生成启动画面..." -ForegroundColor Green

foreach ($screen in $splashScreens) {
    $output = Join-Path $outputDir $screen.name
    $logoSize = [math]::Min($screen.width, $screen.height) / 3
    
    Write-Host "  生成 $($screen.width)x$($screen.height)..." -NoNewline
    
    try {
        # 创建背景
        & magick convert -size "$($screen.width)x$($screen.height)" "xc:$backgroundColor" $output
        
        # 添加居中的logo
        & magick convert $output $logoPath -resize "${logoSize}x${logoSize}" -gravity center -composite $output
        
        Write-Host " 完成" -ForegroundColor Green
    }
    catch {
        Write-Host " 失败: $_" -ForegroundColor Red
    }
}

Write-Host ""
Write-Host "启动画面生成完成!" -ForegroundColor Cyan
