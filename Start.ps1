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
        if($project -eq "WebApp") { Start-Process "http://localhost:5000" }
        
        dotnet run --project $(Join-Path $Using:scriptPath $project) --force
    }
}

Start-Application $PSScriptRoot



