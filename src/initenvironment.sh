#!/bin/bash

# Hi!
# If you're reading this, you're probably interested in what's 
# going on within this script. We've provided what we hope are useful
# comments inline, as well as color-coded relevant shell output.
# We hope it's useful for you, but if you have any questions or suggestions
# please open an issue on https://github.com/MicrosoftDocs/mslearn-maui-consume-rest-services.
#

## Start
declare moduleName=consume-rest-services

# Figure out the name of the resource group to use
declare resourceGroupName=""
declare existingResourceGroup=$(az group list | jq '.[] | select(.tags."x-created-by"=="productsandboxes").name' --raw-output)

# If there is more than one RG or there's only one but its name is not a GUID,
# we're probably not in the Learn sandbox.
if ! [ $existingResourceGroup ]
then
    echo "${warningStyle}WARNING!!!" \
        "It appears you aren't currently running in a Microsoft Learn sandbox." \
        "Any Azure resources provisioned by this script will result in charges" \
        "to your Azure subscription.${defaultTextStyle}"
    resourceGroupName=$moduleName
else
    resourceGroupName=$existingResourceGroup
fi

echo "Using Azure resource group $resourceGroupName."

# Azure
webappname=mslearnpartsserver$RANDOM$RANDOM

cd webservice/PartsServer
az webapp up -n $webappname --resource-group $resourceGroupName --sku FREE --plan $webappname

echo "Web app deployed! Here is the url to use in the app:"
echo https://$webappname.azurewebsites.net
