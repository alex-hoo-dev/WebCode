# PWA 图标生成脚本 (使用 .NET)
# 无需额外安装任何工具

Add-Type -AssemblyName System.Drawing

$sizes = @(16, 32, 72, 96, 128, 144, 152, 167, 180, 192, 384, 512)
$sourceImage = Join-Path $PSScriptRoot "..\logo.png"
$outputDir = $PSScriptRoot

Write-Host "PWA 图标生成脚本 (.NET)" -ForegroundColor Cyan
Write-Host "源图片: $sourceImage"
Write-Host "输出目录: $outputDir"
Write-Host ""

# 检查源文件是否存在
if (-not (Test-Path $sourceImage)) {
    Write-Host "错误: 找不到源图片 $sourceImage" -ForegroundColor Red
    exit 1
}

try {
    $source = [System.Drawing.Image]::FromFile($sourceImage)
    Write-Host "源图片尺寸: $($source.Width)x$($source.Height)" -ForegroundColor Gray
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
            
            # 填充白色背景
            $graphics.Clear([System.Drawing.Color]::White)
            
            # 计算居中位置和缩放
            $scale = [Math]::Min($size / $source.Width, $size / $source.Height)
            $newWidth = [int]($source.Width * $scale)
            $newHeight = [int]($source.Height * $scale)
            $x = [int](($size - $newWidth) / 2)
            $y = [int](($size - $newHeight) / 2)
            
            $graphics.DrawImage($source, $x, $y, $newWidth, $newHeight)
            $graphics.Dispose()
            
            $bitmap.Save($output, [System.Drawing.Imaging.ImageFormat]::Png)
            $bitmap.Dispose()
            
            Write-Host " 完成" -ForegroundColor Green
        }
        catch {
            Write-Host " 失败: $_" -ForegroundColor Red
        }
    }

    $source.Dispose()
    
    Write-Host ""
    Write-Host "图标生成完成!" -ForegroundColor Cyan
    Write-Host "生成的图标位于: $outputDir" -ForegroundColor Gray
}
catch {
    Write-Host "错误: $_" -ForegroundColor Red
    exit 1
}
