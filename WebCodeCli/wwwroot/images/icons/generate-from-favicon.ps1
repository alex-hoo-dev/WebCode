# PWA 图标生成脚本 - 从 favicon.ico 生成
# 使用 .NET 读取 ICO 文件并生成各尺寸 PNG

Add-Type -AssemblyName System.Drawing

$sizes = @(16, 32, 72, 96, 128, 144, 152, 167, 180, 192, 384, 512)
$sourceImage = Join-Path $PSScriptRoot "..\..\favicon.ico"
$outputDir = $PSScriptRoot

Write-Host "PWA 图标生成脚本 - 从 favicon.ico" -ForegroundColor Cyan
Write-Host "源文件: $sourceImage"
Write-Host "输出目录: $outputDir"
Write-Host ""

# 检查源文件是否存在
if (-not (Test-Path $sourceImage)) {
    Write-Host "错误: 找不到源文件 $sourceImage" -ForegroundColor Red
    exit 1
}

$source = $null
$icon = $null

try {
    # 从 ICO 文件加载图标
    $icon = New-Object System.Drawing.Icon($sourceImage, 256, 256)
    $source = $icon.ToBitmap()
    
    Write-Host "源图标尺寸: $($source.Width)x$($source.Height)" -ForegroundColor Gray
}
catch {
    Write-Host "ICO 加载失败，尝试备用方法..." -ForegroundColor Yellow
    try {
        $source = [System.Drawing.Image]::FromFile($sourceImage)
        Write-Host "备用方法成功，源图像尺寸: $($source.Width)x$($source.Height)" -ForegroundColor Gray
    }
    catch {
        Write-Host "无法加载图标文件: $_" -ForegroundColor Red
        exit 1
    }
}

Write-Host ""
Write-Host "开始生成图标..." -ForegroundColor Green

foreach ($size in $sizes) {
    $output = Join-Path $outputDir "icon-${size}x${size}.png"
    Write-Host "  生成 ${size}x${size}..." -NoNewline
    
    try {
        $bitmap = New-Object System.Drawing.Bitmap($size, $size)
        $graphics = [System.Drawing.Graphics]::FromImage($bitmap)
        $graphics.InterpolationMode = [System.Drawing.Drawing2D.InterpolationMode]::HighQualityBicubic
        $graphics.SmoothingMode = [System.Drawing.Drawing2D.SmoothingMode]::HighQuality
        $graphics.PixelOffsetMode = [System.Drawing.Drawing2D.PixelOffsetMode]::HighQuality
        $graphics.CompositingQuality = [System.Drawing.Drawing2D.CompositingQuality]::HighQuality
        
        # 使用透明背景
        $graphics.Clear([System.Drawing.Color]::Transparent)
        
        # 绘制图标，保持比例
        $graphics.DrawImage($source, 0, 0, $size, $size)
        $graphics.Dispose()
        
        $bitmap.Save($output, [System.Drawing.Imaging.ImageFormat]::Png)
        $bitmap.Dispose()
        
        Write-Host " 完成" -ForegroundColor Green
    }
    catch {
        Write-Host " 失败: $_" -ForegroundColor Red
    }
}

# 清理资源
if ($source) { $source.Dispose() }
if ($icon) { $icon.Dispose() }

Write-Host ""
Write-Host "图标生成完成!" -ForegroundColor Cyan
Write-Host "生成的图标位于: $outputDir" -ForegroundColor Gray
