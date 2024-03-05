# But first some ALM setup

| **Goal**                                                                                             | **Estimated duration** |
| ---------------------------------------------------------------------------------------------------- | ---------------------- |
| Implement a GitHub workflow to manage the Application Lifecycle Management (ALM) of our developments | 30 min                 |

> [!NOTE]
> Due to time constraint and because it is not the main goal of this workshop, only one GitHub workflow will be built in this chapter using GitHub Codespaces and GitHub Copilot. The rest of the required workflows are already present in the GitHub repository template that you used to initialize your repository in [chapter 3](./03-InitializeWorkspace.md).

## Start and setup your GitHub Codespace

In your repository created from this GitHub repository template in the [chapter 3](./03-InitializeWorkspace.md) of this workshop, from the **Code** tab,
1. Click on the `Code` button
2. In the `Codespaces` tab, click on the `Create codespace on main` button
3. Wait for the codespace to be initialized and opened in your browser
4. In the extensions tab, search for `GitHub Copilot` and install it
5. In the extensions tab, search for `GitHub Actions` and install it

## Create a GitHub workflow

> [!NOTE]
> We will create a GitHub workflow to automatically build and deploy the code of a Power Platform solution containing a PCF Component.

In your GitHub Codespaces created in the previous step, ...