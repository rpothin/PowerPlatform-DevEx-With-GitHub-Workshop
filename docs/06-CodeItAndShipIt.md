# Code it and ship it

| **Goal**                                                                            | **Estimated duration** |
| ----------------------------------------------------------------------------------- | ---------------------- |
| Master the power of GitHub capabilities to improve your development flow efficiency | 60 min                 |

> [!NOTE]
> This chapter is the continuation of the previous one, please keep the track you selected (_[Frontend development track](#frontend-development-track) or [Backend development track](#backend-development-track)_) to be consistent across the chapters and get a better experience.

## Frontend development track

### Upgrade your your GitHub Codespace

In your GitHub Codespaces created in the previous chapter,

1. Press `Ctrl + Shift + P` to open the command palette
2. Search for the `Codespaces: Add Dev Container Configuration Files...` command and select it
3. Select `Create a new configuration...`
4. In the warning popup, click on the `Continue` button
5. Click on `Show All Definitions...`
6. Search `Node.js` and select a proposed option - _you can for example take the `Node.js & TypeScript (typescript-node)` in `20-bullseye`_
7. Search for `Dotnet CLI`, select the corresponding feature and click on the `OK` button
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
				"ms-dotnettools.dotnet-interactive-vscode"
			]
		}
	}
```

10. Press `Ctrl + Shift + P` to open the command palette
11. Search for the `Codespaces: Rebuild Container` command and select it
12. Wait for the dev container to be initialized and opened in your browser

### Let's code a PCF component

Using the [frontend-development-helper](../src/notebooks/frontend-development-helper.dib) notebook, initialize the codebase for the development of a PCF component.

You can find below some ideas of simple PCF components you could develop during this exercise:
- ...

## Backend development track

### Upgrade your your GitHub Codespace

In your GitHub Codespaces created in the previous chapter,

1. Press `Ctrl + Shift + P` to open the command palette
2. Search for the `Codespaces: Add Dev Container Configuration Files...` command and select it
3. Select `Create a new configuration...`
4. In the warning popup, click on the `Continue` button
5. Click on `Show All Definitions...`
6. Search `.Net` and select a proposed option - _you can for example take the `...` in `...`_
7. Skip the selection of features and click on the `OK` button
8. In the information popup, click on the `Overwrite` button
9. In the `.devcontainer/devcontainer.json` file, add the `customizations` section just after the `image` property, like shown below:

```json
	"image": "...",
	"customizations": {
		"vscode": {
			"extensions": [
				"GitHub.copilot",
				"github.vscode-github-actions",
				"microsoft-IsvExpTools.powerplatform-vscode",
				"ms-dotnettools.dotnet-interactive-vscode"
			]
		}
	}
```

10. Press `Ctrl + Shift + P` to open the command palette
11. Search for the `Codespaces: Rebuild Container` command and select it
12. Wait for the dev container to be initialized and opened in your browser

### Let's code some tests

Using the [backend-development-helper](../src/notebooks/backend-development-helper.dib) notebook, initialize the codebase for the development of a .Net application to test customizations done around Dataverse.

You can find below some ideas of simple tests you could develop during this exercise:
- ...

> [!NOTE]
> For this track, a simple solution with some cloud flows is provided ([...](...)) so you can import it to your Power Platform environment using the `pack-deploy-solution` GitHub workflow available in your repository.

[⬅️ Previous chapter](./05-SomeALMSetup.md) | [🏠 Main agenda](./README.md) | [Next chapter ➡️](./07-JobsNotFinished.md)