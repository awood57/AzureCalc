Calculator and Unit Conversion project using Azure Storage and Azure Functions.

## Running Locally
### 
### Starting Azure Storage
```bash
azurite --silent --location ./azurite --debug ./azurite/debug.log
```
### Starting Azure Functions
```bash
cd AzureCalc.Api/
func start
```
> **Note:** When running locally, you may need to configure CORS in `local.settings.json` to allow requests from the frontend:
> ```json
> {
>   "Host": {
>     "CORS": "http://localhost:5275"
>   }
> }
> ```
### Starting AzureCalc
```bash
cd AzureCalc.Frontend/
dotnet run
```

## Configuration
The frontend API_URL environment variable is set in `launchSettings.json`
```json
"environmentVariables": {
    "API_URL": "http://localhost:7071"
}
```

