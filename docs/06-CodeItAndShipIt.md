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
3. Select the `Node.js` option
4. Wait for the dev container to be initialized and opened in your browser

### Let's code a PCF component

// Initialization of the codebase using a polyglot notebook

// pac solution init --publisherName "Contoso" --publisherPrefix "con" --customizationprefix "con" --solutionName "MyFirstPCFComponent" --outputpath "MyFirstPCFComponent"

// pac pcf init --namespace Contoso --name MyFirstPCFComponent --template field --templateversion 1.0.0

// pac solution add-reference --path .\MyFirstPCFComponent

## Backend development track

### Upgrade your your GitHub Codespace

In your GitHub Codespaces created in the previous chapter,

1. Press `Ctrl + Shift + P` to open the command palette
2. Search for the `Codespaces: Add Dev Container Configuration Files...` command and select it
3. Select the `.NET Core` option
4. Wait for the dev container to be initialized and opened in your browser

### Let's code some tests

// Initialization of the codebase using a polyglot notebook

// dotnet new xunit -n MyFirstTestProject

// dotnet add package Microsoft.PowerPlatform.Dataverse.Client

[⬅️ Previous chapter](./05-SomeALMSetup.md) | [🏠 Main agenda](./README.md) | [Next chapter ➡️](./07-JobsNotFinished.md)