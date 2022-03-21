
$location = "eastus2"

$organization = "ea";
$application = "staticappdemo";
$environment = "dev";

$secretkey = "pulumi-github-token"
$secretValue = "PLACEHOLDER"


$rg = "$($organization)-$($application)-$($environment)-rg-inf"
$kvt = "$($organization)$($application)$($environment)kvtinf"
az group create -n $rg -l $location
az keyvault create -n $kvt -g $rg -l $location
az keyvault secret set --vault-name $kvt -n $secretkey --value $secretValue