$accountName = "nominateandvote"
$table = "pollsubject";
$fileOut = Resolve-Path "movies_parsed.list"
$fileCSV = "movies_parsed.csv"

$null > $fileCSV
$fileCSV = Resolve-Path $fileCSV

Add-AzureAccount > $null
$accountKey = Read-Host 'Please enter your storage account password'
$accountKey = $accountKey.Trim()

$encoding = [System.Text.Encoding]::GetEncoding("ISO-8859-1")
$env:AZURE_STORAGE_CONNECTION_STRING = "DefaultEndpointsProtocol=https;AccountName=$accountName;AccountKey=$accountKey;"

$prompt = Read-Host 'Drop old table (yes/no)?'
if($prompt.Trim().ToLower() -eq "yes") {
    $tables = (Get-AzureStorageTable)
    if($tables -and $tables.name.Contains($table)) {
        Remove-AzureStorageTable -Name $table -Force > $null
        echo "Table deleted"
    }
    
    $i = 1;
    while($i -le 10) {
        $tables = (Get-AzureStorageTable)

        if($tables -and $tables.name.Contains($table)) {
            break
        }

        echo "Trying to create table ($i)"
        Start-Sleep -s 10
        New-AzureStorageTable $table > $null
        $i++
    }
}

$tables = (Get-AzureStorageTable)
if($tables -and $tables.name.Contains($table)) {
    echo "Table has been created successfully / already existed"

    $data = @()
    
    $id = 1
    foreach ($line in [System.IO.File]::ReadLines($fileOut, $encoding)) {
        $parts = $line.Split(' ', 2)
        $year = [int]$parts[0].Trim()
        $title = $parts[1].Trim()

        $row = "" | Select "PartitionKey", "RowKey", "Title", "Year"

        $row.PartitionKey = $id.ToString().PadLeft(8, '0')
        $row.RowKey = ""
        $row.Title = $title
        $row.Year = $year

        $data += $row

        $id++
    }

    $data
    $data | Export-Csv -Path $fileCSV -NoTypeInformation

    echo "Import the generated CSV using Azure Storage Explorer"
} else {
    echo "Unable to create table, maybe deletion has not completed on the servers yet / table does not exist and creation was requested"
}

