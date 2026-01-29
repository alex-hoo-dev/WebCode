# PWA 图标生成脚本
# 需要安装 ImageMagick: winget install ImageMagick.ImageMagick
# 或者使用在线工具如 https://realfavicongenerator.net/

$sizes = @(16, 32, 72, 96, 128, 144, 152, 167, 180, 192, 384, 512)
$sourceImage = Join-Path $PSScriptRoot "..\..\logo.png"
$outputDir = $PSScriptRoot

Write-Host "PWA 图标生成脚本" -ForegroundColor Cyan
Write-Host "源图片: $sourceImage"
Write-Host "输出目录: $outputDir"
Write-Host ""

# 检查源文件是否存在
if (-not (Test-Path $sourceImage)) {
    Write-Host "错误: 找不到源图片 $sourceImage" -ForegroundColor Red
    Write-Host "请确保 images/logo.png 文件存在" -ForegroundColor Yellow
    exit 1
}

# 检查 ImageMagick 是否安装
$magick = Get-Command magick -ErrorAction SilentlyContinue
if (-not $magick) {
    Write-Host "未检测到 ImageMagick，请先安装:" -ForegroundColor Yellow
    Write-Host "  winget install ImageMagick.ImageMagick" -ForegroundColor Green
    Write-Host ""
    Write-Host "或者使用在线工具生成图标:" -ForegroundColor Yellow
    Write-Host "  https://realfavicongenerator.net/" -ForegroundColor Green
    Write-Host "  https://www.pwabuilder.com/imageGenerator" -ForegroundColor Green
    exit 1
}

Write-Host "开始生成图标..." -ForegroundColor Green

foreach ($size in $sizes) {
    $output = Join-Path $outputDir "icon-${size}x${size}.png"
    Write-Host "  生成 ${size}x${size}..." -NoNewline
    
    try {
        & magick convert $sourceImage -resize "${size}x${size}" -background white -gravity center -extent "${size}x${size}" $output
        Write-Host " 完成" -ForegroundColor Green
    }
    catch {
        Write-Host " 失败: $_" -ForegroundColor Red
    }
}

Write-Host ""
Write-Host "图标生成完成!" -ForegroundColor Cyan
Write-Host "请检查 $outputDir 目录下的图标文件" -ForegroundColor Gray
