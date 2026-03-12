param(
    [string]$SourceMd = "ACC GUIA TECNICA - Reload.md",
    [string]$OutputBaseName = "ACC GUIA TECNICA - Reload"
)

$ErrorActionPreference = "Stop"

$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$sourcePath = Join-Path $scriptDir $SourceMd
$outputDocx = Join-Path $scriptDir "$OutputBaseName.docx"
$outputPdf = Join-Path $scriptDir "$OutputBaseName.pdf"

if (-not (Test-Path $sourcePath)) {
    throw "No existe el archivo fuente: $sourcePath"
}

$pandoc = Get-Command pandoc -ErrorAction SilentlyContinue
if (-not $pandoc) {
    throw "Se requiere 'pandoc' en PATH para generar DOCX/PDF."
}

Write-Host "Generando DOCX..."
& pandoc $sourcePath --from gfm --standalone --output $outputDocx

$pdfEngine = $null
foreach ($candidate in @("wkhtmltopdf", "xelatex", "pdflatex")) {
    if (Get-Command $candidate -ErrorAction SilentlyContinue) {
        $pdfEngine = $candidate
        break
    }
}

if (-not $pdfEngine) {
    throw "No se encontro motor PDF. Instala wkhtmltopdf o xelatex/pdflatex."
}

Write-Host "Generando PDF con engine: $pdfEngine"
& pandoc $sourcePath --from gfm --standalone --pdf-engine $pdfEngine --output $outputPdf

Write-Host "Listo."
Write-Host "DOCX: $outputDocx"
Write-Host "PDF:  $outputPdf"

