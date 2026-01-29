# PWA 启动画面生成脚本 (使用 .NET)
# 无需额外安装任何工具

Add-Type -AssemblyName System.Drawing

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
$backgroundColor = [System.Drawing.ColorTranslator]::FromHtml("#2563eb")

Write-Host "PWA 启动画面生成脚本 (.NET)" -ForegroundColor Cyan
Write-Host ""

# 检查源文件是否存在
if (-not (Test-Path $logoPath)) {
    Write-Host "错误: 找不到Logo图片 $logoPath" -ForegroundColor Red
    exit 1
}

try {
    $logo = [System.Drawing.Image]::FromFile($logoPath)
    Write-Host "Logo尺寸: $($logo.Width)x$($logo.Height)" -ForegroundColor Gray
    Write-Host ""
    Write-Host "开始生成启动画面..." -ForegroundColor Green

    foreach ($screen in $splashScreens) {
        $output = Join-Path $outputDir $screen.name
        $logoSize = [Math]::Min($screen.width, $screen.height) / 3
        
        Write-Host "  生成 $($screen.width)x$($screen.height)..." -NoNewline
        
        try {
            $bitmap = New-Object System.Drawing.Bitmap($screen.width, $screen.height)
            $graphics = [System.Drawing.Graphics]::FromImage($bitmap)
            $graphics.InterpolationMode = [System.Drawing.Drawing2D.InterpolationMode]::HighQualityBicubic
            $graphics.SmoothingMode = [System.Drawing.Drawing2D.SmoothingMode]::HighQuality
            
            # 填充背景色
            $graphics.Clear($backgroundColor)
            
            # 计算Logo位置和大小
            $scale = [Math]::Min($logoSize / $logo.Width, $logoSize / $logo.Height)
            $newWidth = [int]($logo.Width * $scale)
            $newHeight = [int]($logo.Height * $scale)
            $x = [int](($screen.width - $newWidth) / 2)
            $y = [int](($screen.height - $newHeight) / 2)
            
            # 绘制Logo
            $graphics.DrawImage($logo, $x, $y, $newWidth, $newHeight)
            
            # 添加应用名称
            $font = New-Object System.Drawing.Font("Segoe UI", 24, [System.Drawing.FontStyle]::Bold)
            $brush = [System.Drawing.Brushes]::White
            $appName = "WebCode"
            $textSize = $graphics.MeasureString($appName, $font)
            $textX = ($screen.width - $textSize.Width) / 2
            $textY = $y + $newHeight + 30
            $graphics.DrawString($appName, $font, $brush, $textX, $textY)
            $font.Dispose()
            
            $graphics.Dispose()
            $bitmap.Save($output, [System.Drawing.Imaging.ImageFormat]::Png)
            $bitmap.Dispose()
            
            Write-Host " 完成" -ForegroundColor Green
        }
        catch {
            Write-Host " 失败: $_" -ForegroundColor Red
        }
    }

    $logo.Dispose()
    
    Write-Host ""
    Write-Host "启动画面生成完成!" -ForegroundColor Cyan
    Write-Host "生成的文件位于: $outputDir" -ForegroundColor Gray
}
catch {
    Write-Host "错误: $_" -ForegroundColor Red
    exit 1
}
