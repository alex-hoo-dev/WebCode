# PWA Icon Generator - with blue background
# Creates icons suitable for mobile home screens

Add-Type -AssemblyName System.Drawing

$sizes = @(16, 32, 72, 96, 128, 144, 152, 167, 180, 192, 384, 512)
$sourceImage = Join-Path $PSScriptRoot "..\..\favicon.ico"
$outputDir = $PSScriptRoot
$backgroundColor = [System.Drawing.ColorTranslator]::FromHtml("#2563eb")

Write-Host "PWA Icon Generator - with blue background" -ForegroundColor Cyan
Write-Host "Source: $sourceImage"
Write-Host "Output: $outputDir"
Write-Host ""

if (-not (Test-Path $sourceImage)) {
    Write-Host "Error: Source file not found" -ForegroundColor Red
    exit 1
}

$source = $null
$icon = $null

try {
    $icon = New-Object System.Drawing.Icon($sourceImage, 256, 256)
    $source = $icon.ToBitmap()
    Write-Host "Source icon size: $($source.Width)x$($source.Height)" -ForegroundColor Gray
}
catch {
    Write-Host "ICO loading failed, trying fallback..." -ForegroundColor Yellow
    try {
        $source = [System.Drawing.Image]::FromFile($sourceImage)
        Write-Host "Fallback succeeded, size: $($source.Width)x$($source.Height)" -ForegroundColor Gray
    }
    catch {
        Write-Host "Cannot load icon: $_" -ForegroundColor Red
        exit 1
    }
}

Write-Host ""
Write-Host "Generating icons with blue background..." -ForegroundColor Green

foreach ($size in $sizes) {
    $output = Join-Path $outputDir "icon-${size}x${size}.png"
    Write-Host "  Generating ${size}x${size}..." -NoNewline
    
    try {
        $bitmap = New-Object System.Drawing.Bitmap($size, $size)
        $graphics = [System.Drawing.Graphics]::FromImage($bitmap)
        $graphics.InterpolationMode = [System.Drawing.Drawing2D.InterpolationMode]::HighQualityBicubic
        $graphics.SmoothingMode = [System.Drawing.Drawing2D.SmoothingMode]::HighQuality
        $graphics.PixelOffsetMode = [System.Drawing.Drawing2D.PixelOffsetMode]::HighQuality
        $graphics.CompositingQuality = [System.Drawing.Drawing2D.CompositingQuality]::HighQuality
        
        # Fill blue background
        $graphics.Clear($backgroundColor)
        
        # Calculate icon area with padding
        $padding = [int]($size * 0.1)
        $iconSize = $size - ($padding * 2)
        
        # Draw icon
        $graphics.DrawImage($source, $padding, $padding, $iconSize, $iconSize)
        $graphics.Dispose()
        
        $bitmap.Save($output, [System.Drawing.Imaging.ImageFormat]::Png)
        $bitmap.Dispose()
        
        Write-Host " Done" -ForegroundColor Green
    }
    catch {
        Write-Host " Failed: $_" -ForegroundColor Red
    }
}

if ($source) { $source.Dispose() }
if ($icon) { $icon.Dispose() }

Write-Host ""
Write-Host "Icon generation completed!" -ForegroundColor Cyan
Write-Host "Icons saved to: $outputDir" -ForegroundColor Gray
