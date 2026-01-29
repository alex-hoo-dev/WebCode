# PWA Splash Screen Generator - Clean version
# Creates splash screens with solid blue background and centered logo

Add-Type -AssemblyName System.Drawing

$splashScreens = @(
    @{ width = 640; height = 1136; name = "splash-640x1136.png" },
    @{ width = 750; height = 1334; name = "splash-750x1334.png" },
    @{ width = 1242; height = 2208; name = "splash-1242x2208.png" },
    @{ width = 1125; height = 2436; name = "splash-1125x2436.png" },
    @{ width = 1242; height = 2688; name = "splash-1242x2688.png" },
    @{ width = 828; height = 1792; name = "splash-828x1792.png" }
)

$sourceImage = Join-Path $PSScriptRoot "..\..\favicon.ico"
$outputDir = $PSScriptRoot
$backgroundColor = [System.Drawing.ColorTranslator]::FromHtml("#2563eb")

Write-Host "PWA Splash Screen Generator - Clean version" -ForegroundColor Cyan
Write-Host "Source: $sourceImage"
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
    try {
        $source = [System.Drawing.Image]::FromFile($sourceImage)
        Write-Host "Fallback succeeded, size: $($source.Width)x$($source.Height)" -ForegroundColor Gray
    }
    catch {
        Write-Host "Cannot load icon: $_" -ForegroundColor Red
        exit 1
    }
}

# Create inverted version (white on transparent for dark backgrounds)
function Invert-Image {
    param([System.Drawing.Bitmap]$img)
    
    $result = New-Object System.Drawing.Bitmap($img.Width, $img.Height)
    
    for ($x = 0; $x -lt $img.Width; $x++) {
        for ($y = 0; $y -lt $img.Height; $y++) {
            $pixel = $img.GetPixel($x, $y)
            
            # If pixel is dark (black logo), make it white
            # If pixel is light (white background), make it transparent
            $brightness = ($pixel.R + $pixel.G + $pixel.B) / 3
            
            if ($brightness -lt 128) {
                # Dark pixel -> white
                $result.SetPixel($x, $y, [System.Drawing.Color]::FromArgb($pixel.A, 255, 255, 255))
            } else {
                # Light pixel -> transparent
                $result.SetPixel($x, $y, [System.Drawing.Color]::FromArgb(0, 255, 255, 255))
            }
        }
    }
    
    return $result
}

Write-Host "Creating inverted logo for splash screens..." -ForegroundColor Gray
$invertedLogo = Invert-Image -img $source

Write-Host ""
Write-Host "Generating splash screens..." -ForegroundColor Green

foreach ($screen in $splashScreens) {
    $output = Join-Path $outputDir $screen.name
    $logoSize = [Math]::Min($screen.width, $screen.height) / 3
    
    Write-Host "  Generating $($screen.width)x$($screen.height)..." -NoNewline
    
    try {
        $bitmap = New-Object System.Drawing.Bitmap($screen.width, $screen.height)
        $graphics = [System.Drawing.Graphics]::FromImage($bitmap)
        $graphics.InterpolationMode = [System.Drawing.Drawing2D.InterpolationMode]::HighQualityBicubic
        $graphics.SmoothingMode = [System.Drawing.Drawing2D.SmoothingMode]::HighQuality
        
        # Fill blue background
        $graphics.Clear($backgroundColor)
        
        # Calculate logo position (centered)
        $scale = [Math]::Min($logoSize / $invertedLogo.Width, $logoSize / $invertedLogo.Height)
        $newWidth = [int]($invertedLogo.Width * $scale)
        $newHeight = [int]($invertedLogo.Height * $scale)
        $x = [int](($screen.width - $newWidth) / 2)
        $y = [int](($screen.height - $newHeight) / 2)
        
        # Draw inverted logo (white on blue)
        $graphics.DrawImage($invertedLogo, $x, $y, $newWidth, $newHeight)
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
if ($invertedLogo) { $invertedLogo.Dispose() }

Write-Host ""
Write-Host "Splash screen generation completed!" -ForegroundColor Cyan
Write-Host "Files saved to: $outputDir" -ForegroundColor Gray
