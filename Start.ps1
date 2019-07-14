workflow Start-Application
{
    param(
        [Parameter(Mandatory=$True, Position=1)]
        [string] $scriptPath
    )

    $projectsDir = @(
        "Services\Inventory",
        "Services\Basket",
        "Services\Pricing"
        "GatewayApi",
        "WebApp"
    )

    foreach -Parallel ($project in $projectsDir)
    {
        Write-Output "Run project $project"        
        dotnet run --project $(Join-Path $Using:scriptPath $project) --force
    }
}

Start-Job -Name "launch browser" -ScriptBlock {
    function Test-Port
    {
        param
        (
            $Address,
            $Port
        )

        $tcpClient = new-object Net.Sockets.TcpClient
        try
        {
            $tcpClient.Connect("$Address", $Port)
            $true
        }
        catch
        {
            $false
        }
        finally
        {
            $tcpClient.Dispose()
        }
    }

    do{

        Start-Sleep -Seconds 5       
    }while(-not (Test-Port -Address localhost -Port 5000))
    
    Start-Process "http://localhost:5000"
}

Start-Application $PSScriptRoot
