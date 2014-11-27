$file = Resolve-Path "movies.list"
$fileCSV = "movies.csv"
$fileSkip = "movies_skip.txt"
$patten = '^([A-Za-z0-9 -,].*)\s\((\d+)[IV/]*\)\s.*'

# use a hash set to avoid duplicates
$set = New-Object -TypeName System.Collections.Generic.HashSet[string]

$null > $fileCSV
$null > $fileSkip

$fileCSV = Resolve-Path $fileCSV
$fileSkip = Resolve-Path $fileSkip

$encoding = [System.Text.Encoding]::GetEncoding("ISO-8859-1")

try {
    $streamMovies = New-Object -TypeName System.IO.StreamWriter -ArgumentList @($fileCSV, $false, $encoding)
    $streamSkip = [System.IO.StreamWriter] $fileSkip.ToString()
    $i = 1;
    foreach ($line in [System.IO.File]::ReadLines($file, $encoding)) {
        if($line -match $patten) {
            $title = $Matches[1].Trim()
            $year = $Matches[2].Trim()

            if($title.StartsWith('"')) {
                $title = $title.Substring(1, $title.Length-2)
            }

            $csvLine = '"' + $title + '","' + $year + '"'

            if($set.Add($csvLine)) {
                $streamMovies.WriteLine($csvLine)
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
