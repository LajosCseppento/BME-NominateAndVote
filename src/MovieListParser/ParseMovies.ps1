$file = Resolve-Path "movies.list"
$fileOut = "movies_parsed.list"
$fileSkip = "movies_skip.list"
$patten = '^([A-Za-z0-9 -,].*)\s\((\d+)[IV/]*\)\s.*'

# use a hash set to avoid duplicates
$set = New-Object -TypeName System.Collections.Generic.HashSet[string]

$null > $fileOut
$null > $fileSkip

$fileOut = Resolve-Path $fileOut
$fileSkip = Resolve-Path $fileSkip

$encoding = [System.Text.Encoding]::GetEncoding("ISO-8859-1")

try {
    $streamMovies = New-Object -TypeName System.IO.StreamWriter -ArgumentList @($fileOut, $false, $encoding)
    $streamSkip = [System.IO.StreamWriter] $fileSkip.ToString()
    $i = 1;
    foreach ($line in [System.IO.File]::ReadLines($file, $encoding)) {
        if($line -match $patten) {
            $title = $Matches[1].Trim()
            $year = $Matches[2].Trim()

            if($title.StartsWith('"')) {
                $title = $title.Substring(1, $title.Length-2)
            }

            $outLine = $year + ' ' + $title # in this format reading+splitting will be faster

            if($set.Add($outLine)) {
                $streamMovies.WriteLine($outLine)
            }
        } else {
            # no match, skip
            $streamSkip.WriteLine($line)
        }

        $i++;
        if($i -gt 5000) {
            $streamMovies.Flush();
            $streamSkip.Flush();
            $i = 1;
        }
    }

    $streamMovies.Flush();
    $streamSkip.Flush();
} finally {
    $streamMovies.Close()
    $streamSkip.Close()
}
