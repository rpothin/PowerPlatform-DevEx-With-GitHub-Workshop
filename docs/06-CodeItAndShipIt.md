# 06 - Code it and ship it!

| **Goal**                                                                            | **Estimated duration** |
| ----------------------------------------------------------------------------------- | ---------------------- |
| Master the power of GitHub capabilities to improve your development flow efficiency | 60 min                 |

> [!NOTE]
> This chapter is the continuation of the previous one. Please keep the track you selected (_[Frontend development track](#frontend-development-track) or [Backend development track](#backend-development-track)_) to be consistent across the chapters and get a better experience.

## Frontend development track

### Upgrade your GitHub Codespace

In your GitHub Codespace created in the previous chapter,

1. Press `Ctrl + Shift + P` to open the command palette
2. Search for the `Codespaces: Add Dev Container Configuration Files...` command and select it
3. Select `Create a new configuration...`
4. In the warning popup, click on the `Continue` button
5. Click on `Show All Definitions...`
6. Search `Node.js` and select a proposed option - _you can for example take the `Node.js & TypeScript (typescript-node)` in `20-bullseye`_
7. Search for `Dotnet CLI` and `GitHub CLI`, select the corresponding feature and click on the `OK` button
8. In the information popup, click on the `Overwrite` button
9. In the `.devcontainer/devcontainer.json` file, add the `customizations` section just after the `image` property, like shown below:

```json
	"image": "mcr.microsoft.com/devcontainers/typescript-node:1-20-bullseye",
	"customizations": {
		"vscode": {
			"extensions": [
				"GitHub.copilot",
				"github.vscode-github-actions",
				"microsoft-IsvExpTools.powerplatform-vscode",
				"ms-dotnettools.dotnet-interactive-vscode",
				"yzhang.markdown-all-in-one"
			]
		}
	},
	"features": {
		"ghcr.io/devcontainers/features/dotnet:2": {},
		"ghcr.io/devcontainers/features/github-cli:1": {}
	}
```

> [!NOTE]
> Regarding the extensions, you could also consider the following one that could help you during the development of your PCF component: [PCF Builder by Danish Naglekar](https://marketplace.visualstudio.com/items?itemName=danish-naglekar.pcf-builder)

10. Press `Ctrl + Shift + P` to open the command palette
11. Search for the `Codespaces: Rebuild Container` command and select it
12. Wait for the dev container to be initialized and opened in your browser

### Let's code a PCF component

1. Using the [frontend-development-helper](../src/notebooks/frontend-development-helper.dib) notebook, initialize the codebase for the development of a PCF component.

> [!WARNING]
> The solution name considered during the configuration of the GitHub workflow for this track was `PCFComponents`, so you will need to use this name when prompted by the notebook.

2. Develop a simple PCF component using all the support you can get from GitHub Copilot (_code suggestion, code explaination, documentation, fix issues..._).
3. Test your PCF component from your GitHub Codespace - _[Debug code components](https://learn.microsoft.com/en-us/power-apps/developer/component-framework/debugging-custom-controls)_.
4. Commit and push your changes to the `main` branch of your repository - _this will trigger the GitHub workflow you created in the previous step to build and deploy your solution with the PCF component to your Power Platform environment_.

> [!NOTE]
> If the workflow does not work as expected, you can add the [`workflow_dispatch`](https://docs.github.com/en/actions/using-workflows/events-that-trigger-workflows#workflow_dispatch) event in the trigger section of the workflow to be able to run it manually and debug the issue.

5. Configure the PCF component in your Power Platform environment and test it.

> [!NOTE]
> You can find below some ideas of simple PCF components you could develop during this exercise:
> - Present the Interpol Red Notice status of a contact using the [Interpol Notices API](https://interpol.api.bund.dev/)
> - Present the reputation of the email address of a contact using the [EmailRep API](https://emailrep.io/)
> - Present details about the website of a company (_in the account table_) using the [LinkPreview API](https://www.linkpreview.net/)
> - Present security details regarding the domain of the website of a company (_in the account table_) using the [FullHunt API](https://api-docs.fullhunt.io/)
> - Use the [Faker API](https://fakerapi.it/en) or the [Random Data API](https://random-data-api.com/) combined with an environment variable (_as a flag_) to simplify the generation of fake data for testing purposes

## Backend development track

### Upgrade your your GitHub Codespace

In your GitHub Codespace created in the previous chapter,

1. Press `Ctrl + Shift + P` to open the command palette
2. Search for the `Codespaces: Add Dev Container Configuration Files...` command and select it
3. Select `Create a new configuration...`
4. In the warning popup, click on the `Continue` button
5. Click on `Show All Definitions...`
6. Search `.Net` and select a proposed option - _you can for example take the `C# (.NET) (dotnet)` in `8.0-bookworm`_
7. Search for `Dotnet CLI` and `GitHub CLI`, select the corresponding feature and click on the `OK` button, then let you guide and select default options
8. In the information popup, click on the `Overwrite` button
9. In the `.devcontainer/devcontainer.json` file, add the `customizations` section just after the `image` property, like shown below:

```json
	"image": "mcr.microsoft.com/devcontainers/dotnet:1-8.0-bookworm",
	"customizations": {
		"vscode": {
			"extensions": [
				"GitHub.copilot",
				"github.vscode-github-actions",
				"microsoft-IsvExpTools.powerplatform-vscode",
				"ms-dotnettools.dotnet-interactive-vscode",
				"yzhang.markdown-all-in-one"
			]
		}
	},
	"features": {
		"ghcr.io/devcontainers/features/dotnet:2": {},
		"ghcr.io/devcontainers/features/github-cli:1": {}
	}
```

10. Press `Ctrl + Shift + P` to open the command palette
11. Search for the `Codespaces: Rebuild Container` command and select it
12. Wait for the dev container to be initialized and opened in your browser

### Power Platform solution to test

#### Import the solution to your environment

1. In your Power Platform environment, create a `Microsoft Dataverse` connection and copy its ID (_from the URL of the connection page_)
2. In your GitHub Codespace, open the `resources/06-CodeItAndShipIt/backend-development/VirtualPetSimulator/deployment-settings.json` file
3. Line 40, add the `connectionId` you copied in the previous step
4. Commit and push your changes to the `main` branch of your repository
5. Go to the `Actions` page of your repository
6. In the `All workflows` section, click on the `Build and deploy solution` workflow
7. Click on the `Run workflow` button
8. In the popup, click on the `Run workflow` button
9. Follow the logs of the workflow to ensure it completes successfully
10. Go to your Power Platform environment to validate that you see a `Virtual Pet Simulator` solution under the `Managed` group

#### Requirements covered by the solution

`Virtual Pet Simulator` is a simple (_really simple_) game where you create a pet and take care of it. The pet has life and happiness points. You can feed it and cuddle it.

Here are the main requirements covered by the solution:
- As a user, when I create a pet with a name I want to have its life and happiness points asynchronously initialized to 100 000
- Every minute, the life and happiness points of every pet decrease by 10
- As a user, when I feed my pet by selecting a quantity of food, I want to have its life points increased by the quantity of food selected - but it can not exceed 100 000 (_initial value_)
- As a user, when I cuddle my pet, I want to have its happiness points increased by 1 000 - but it can not exceed 100 000 (_initial value_)

### Let's code some tests

1. Using the [backend-development-helper](../src/notebooks/backend-development-helper.dib) notebook, initialize the codebase for the development of a .Net application to test customizations done around Dataverse.

> [!WARNING]
> The name of the .Net solution considered during the configuration of the GitHub workflow for this track was `Dataverse.API.Testing`, so you will need to use this name when prompted by the notebook.

2. Develop tests for the `Virtual Pet Simulator` solution to ensure that the documented requirements are met.
3. Commit and push your changes to the `main` branch of your repository to validate your tests - _this will trigger the GitHub workflow you created in the previous step to build and run the tests considering your Power Platform environment where the solution is deployed_.

> [!NOTE]
> If the workflow does not work as expected, you can add the [`workflow_dispatch`](https://docs.github.com/en/actions/using-workflows/events-that-trigger-workflows#workflow_dispatch) event in the trigger section of the workflow to be able to run it manually and debug the issue.

[⬅️ Previous chapter](./05-SomeALMSetup.md) | [🏡 Main agenda](../README.md#workshop-agenda) | [Next chapter ➡️](./07-JobsNotFinished.md)