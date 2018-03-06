# ShakeShake

## Steps to start

1. Setup development environment by following [Get Started](https://microsoft.github.io/azure-iot-developer-kit/docs/get-started/)
2. Open VS Code
3. Invoke command of `IoTWorkbench: Examples` and select ShakeShake

## Provision Azure Services

1. Click **Command** menu in Visual Studio Code - **IoT Workbench: Azure Provision**
2. Select a subscription
3. Select or choose a resource group 
4. Select or create an IoT Hub
5. Wait for the deployment.
6. Create Function App.
7. Wait for the deployment.

## Deploy Function App

1. Click **Command** menu in Visual Studio Code - **IoT Workbench: Azure Deploy**
2. Wait for function app code uploading

## Configure IoT Hub Device Connection String in DevKit

1. Connect your DevKit to your machine
2. Click **Command** menu in Visual Studio Code - **IoT Workbench: config-device-connection**
3. Hold button A on DevKit, then press rest button, and then release button A to enter config mode
4. Wait for connection string configuration

## Uploade Arduino Code to DevKit

1. Connect your DevKit to your machine
2. Click **Command** menu in Visual Studio Code - **IoT Workbench: Device Upload**
3. Wait for arduino code uploading