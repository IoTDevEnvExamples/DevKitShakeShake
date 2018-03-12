# ShakeShake

## Steps to start

1. Setup development environment by following [Get Started](https://microsoft.github.io/azure-iot-developer-kit/docs/get-started/)
2. Open VS Code
3. Press **F1** or **Ctrl + Shift + P** - `IoT Workbench: Examples` and select ShakeShake

## Provision Azure Services

1. Press **F1** or **Ctrl + Shift + P** in Visual Studio Code - **IoT Workbench:Cloud** and click **Azure Provision**
2. Select a subscription.
3. Select or choose a resource group.
4. Select or create an IoT Hub.
5. Wait for the deployment.
6. Select or create an IoT Hub device. Please take a note of the device name.
7. Create Function App.
8. Wait for the deployment.

## Deploy Function App
1. Open shakeshake\run.csx and modify line 91 with the IoT Hub device you created.
2. ress **F1** or **Ctrl + Shift + P** in Visual Studio Code - **IoT Workbench: Cloud** and click **Azure Deploy**.
3. Wait for function app code uploading.

## Configure IoT Hub Device Connection String in DevKit

1. Connect your DevKit to your machine.
2. Press **F1** or **Ctrl + Shift + P** in Visual Studio Code - **IoT Workbench: Device** and click **config-device-connection**.
3. Hold button A on DevKit, then press rest button, and then release button A to enter config mode.
4. Wait for connection string configuration to complete.

## Uploade Arduino Code to DevKit

1. Connect your DevKit to your machine.
2. Press **F1** or **Ctrl + Shift + P** in Visual Studio Code - **IoT Workbench:Device** and click **Device Upload**.
3. Wait for arduino code uploading.