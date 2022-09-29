$url = "http://archive.ics.uci.edu/ml/machine-learning-databases/reuters21578-mld/reuters21578.tar.gz";
$filename = "reuters21578.tar.gz";
$inner = $filename.Replace(".gz", "");
$basename = $inner.Replace(".tar", "");

if (-not (Test-Path $filename -PathType Leaf)) {
  Write-Output "Downloading $filename...";
  Invoke-WebRequest -Uri $url -OutFile $filename;
}

if (-not (Test-Path $filename -PathType Leaf)) {
  Write-Error "Error downloading file $filename.";
  exit;
}

if (-not (Get-Command "7z" -ErrorAction SilentlyContinue)) {
  Write-Error "7z is not installed.";
  exit;
}

if (Test-Path reuters21578) {
  Remove-Item .\reuters21578 -Recurse
}

Write-Output "Extracting from $filename...";

7z x $filename "-o$basename"
7z x (Join-Path $basename $inner) "-o$basename"

Remove-Item (Join-Path $basename $inner);

Write-Output "Done."