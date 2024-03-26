# But first some ALM setup

| **Goal**                                                                                             | **Estimated duration** |
| ---------------------------------------------------------------------------------------------------- | ---------------------- |
| Implement a GitHub workflow to manage the Application Lifecycle Management (ALM) of our developments | 30 min                 |

## Configure an environment in your repository

> [!IMPORTANT]
> At this point, you will need to have at least one Power Platform environment available with an application user configured with the "System Administrator" role and access to its tenant id, client id and secret.

In your repository created from this GitHub repository template in the [chapter 3](./03-InitializeWorkspace.md) of this workshop,

1. Go to the `Settings` tab of your repository
2. Click on the `Environments` menu
3. Click on the `New environment` button
4. In the `Name` field, enter `Sandbox`
5. Click on the `Configure environment` button
6. In the `Environment secrets` section, click on the `Add secret` button
7. In the `Name` field, enter `TENANT_ID`
8. In the `Value` field, enter the tenant id of the application user with the "System Administrator" role in the considered Power Platform environment
9.  Click on the `Add secret` button
10. In the `Environment secrets` section, click on the `Add secret` button
11. In the `Name` field, enter `CLIENT_ID`
12. In the `Value` field, enter the client id of the application user with the "System Administrator" role in the considered Power Platform environment
13. Click on the `Add secret` button
14. In the `Environment secrets` section, click on the `Add secret` button
15. In the `Name` field, enter `CLIENT_SECRET`
16. In the `Value` field, enter the client secret of the application user with the "System Administrator" role in the considered Power Platform environment
17. Click on the `Add secret` button
18. In the `Environment variables` section, click on the `Add variable` button
19. In the `Name` field, enter `DATAVERSE_ENVIRONMENT_URL`
20. In the `Value` field, enter the URL of the considered Power Platform environment
21. Click on the `Add variable` button

## Start and setup your GitHub Codespace

In your repository created from this GitHub repository template in the [chapter 3](./03-InitializeWorkspace.md) of this workshop, from the **Code** tab,

1. Click on the `Code` button
2. In the `Codespaces` tab, click on the `Create codespace on main` button
3. Wait for the codespace to be initialized and opened in your browser

> [!NOTE]
> The dev container configuration coming by default with this GitHub template repository is already configured with a few extensions that will be used during the labs of this workshop.

## Create a GitHub workflow

First, let select the track you want to follow for the next steps:

- [**Frontend development track**](#frontend-development-track): If you want to focus on the development of PCF components
- [**Backend development track**](#backend-development-track): If you want to focus on the development of a .Net project for the testing of customizations around Dataverse using its API

> [!NOTE]
> If you finish the selected track before the time allocated to this chapter, you can come back here and follow the steps for the other track.

### Frontend development track

In your GitHub Codespaces created in the previous step,

1. Add a new file named `pcf-components-ci.yml` under the `.github/workflows` folder
2. Copy and paste the comment block below in the file created in the previous step

```yaml
# This is a GitHub Actions workflow for Continuous Integration (CI) of PCF components.
# The workflow is triggered on every push only under the "src/pcf-components" folder to the main branch.
# 
# The workflow performs the following steps:
# 1. Setup: It sets up the required environment with the necessary versions of Node.js and npm.
# 3. List the changed files under the "src/pcf-components" folder in the latest commit and identify the parent folder of the changed files to get the name of changed PCF component and put it in the environment variable.
# 3. Install Dependencies: It installs all the dependencies defined in the package.json file.
# 4. Build: It builds the PCF components located under the "src/pcf-components" folder using the 'npm run build' command - we can have multiple PCF components implemented there.
# 5. Test: It runs unit tests using the 'npm test' command.
# 6. Setup msbuild: Using the microsoft/setup-msbuild@v2 GitHub action to install msbuild on the runner.
# 7. Pack solution: Run msbuild after installing it on the "src/solutions/PCFComponents" folder to create the solution zip file.
# 8. Store the packed solution zip file as an artifact.
# 9. Deploy: It deploys the solution zip file to the Dataverse environment using the "microsoft/powerplatform-actions/import-solution@main" GitHub action leveraging the "DATAVERSE_ENVIRONMENT_URL", "CLIENT_ID" and "CLIENT_SECRET" GitHub environment secrets and variables.
```

3. Press `Enter` and let you be guided by the GitHub Copilot to complete the workflow file

> [!TIP]
> You can use the `Ctrl + i` keyboard shortcut combined with the `/doc` or `/explain` commands to explore how GitHub Copilot can help you even more in the context of this file.

4. Once the file is completed, commit and push the changes to the repository

> [!WARNING]
> To simplify this configuration you can push the changes directly to the `main` branch, but in a real-world scenario, you should create a new branch, push the changes to it, and then create a pull request to merge the changes into the `main` branch.

5. Validate that the workflow appears in the `Actions` tab of your repository

> [!NOTE]
> In the next chapter, we will implement a PCF component and test the workflow created in this chapter.

### Backend development track

In your GitHub Codespaces created in the previous step,

1. Add a new file named `dataverse-api-testing.yml` under the `.github/workflows` folder
2. Copy and paste the comment block below in the file created in the previous step

```yaml
# This is a GitHub Actions workflow for testing customizations implemented around Dataverse using its API.
# The workflow is triggered on every push only under the "src/dataverse-api-testing" folder to the main branch.
# 
# The workflow performs the following steps:
# 1. Setup: It sets up the required environment with .NET 8.0.
# 2. Restore Dependencies: It restores all the dependencies and tools of the .NET project using the 'dotnet restore' command.
# 3. Test: It runs API tests using the 'dotnet test' command using the "DATAVERSE_ENVIRONMENT_URL", "CLIENT_ID" and "CLIENT_SECRET" GitHub environment secrets and variables in the repository to be able to use the Dataverse API in the considered Test environment.
# 4. Publish Test Results: Use the "dorny/test-reporter@main" GitHub action using the trx file generated by the test command and the "dotnet-trx" reporter to get a nice report of the test results.
```

3. Press `Enter` and let you be guided by the GitHub Copilot to complete the workflow file

> [!TIP]
> You can use the `Ctrl + i` keyboard shortcut combined with the `/doc` or `/explain` commands to explore how GitHub Copilot can help you even more in the context of this file.

4. Once the file is completed, commit and push the changes to the repository

> [!WARNING]
> To simplify this configuration you can push the changes directly to the `main` branch, but in a real-world scenario, you should create a new branch, push the changes to it, and then create a pull request to merge the changes into the `main` branch.

5. Validate that the workflow appears in the `Actions` tab of your repository

> [!NOTE]
> In the next chapter, we will implement a .Net project and test the workflow created in this chapter.

[⬅️ Previous chapter](./04-OrganizeYourWork.md) | [🏡 README](../README.md) | [➡️ Next chapter](./06-CodeItAndShipIt.md)
