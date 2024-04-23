# 05 - But first some ALM setup

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
# The workflow runs on a Windows runner and uses the "Sandbox" environment.
# 
# The workflow performs the following steps:
# 0. Enable git long paths: It enables long paths in git to avoid the "Filename too long" error.
# 1. Setup: It sets up the required environment with the necessary versions of Node.js and npm.
# 3. List the changed files under the "src/pcf-components" folder in the latest commit and identify the parent folder of the changed files to get the name of changed PCF component and put it in the environment variable.
# 3. Install Dependencies: It installs all the dependencies defined in the package.json file.
# 4. Build: It builds the PCF components located under the "src/pcf-components" folder using the 'npm run build' command - we can have multiple PCF components implemented there.
# 5. Test: It runs unit tests using the 'npm test' command.
# 6. Setup msbuild: Using the microsoft/setup-msbuild@v2 GitHub action to install msbuild on the runner.
# 7. Pack solution: Run msbuild with Release configuration after installing it on the "src/solutions/PCFComponents" folder to create the solution zip file.
# 8. Store the packed solution zip file as an artifact.
# 9. Install the Power Platform CLI using the "microsoft/powerplatform-actions/actions-install@main" GitHub action.
# 10. Deploy: It deploys the solution zip file to the Dataverse environment using the "microsoft/powerplatform-actions/import-solution@main" GitHub action leveraging the "TENANT_ID", "CLIENT_ID" and "CLIENT_SECRET" GitHub environment secrets and the "DATAVERSE_ENVIRONMENT_URL" environment variable.
```

3. Press `Enter` and let you be guided by GitHub Copilot to complete the workflow file

> [!TIP]
> You can use the `Ctrl + i` keyboard shortcut combined with the `/doc` or `/explain` commands to explore how GitHub Copilot can help you even more in the context of this file.

> [!WARNING]
> GitHub Copilot suggestions are not always valid, so be careful and validate the code generated by it.
> We will use the time allocated to the next chapter and the fact that you will have some code to test to finalize the configuration of the GitHub workflow and make it work, don't worry.
> You can add the [`workflow_dispatch`](https://docs.github.com/en/actions/using-workflows/events-that-trigger-workflows#workflow_dispatch) event in the trigger section of the workflow to be able to run it manually and investigate why it is not working as expected.

4. Once the file is completed, commit and push the changes to the repository

5. Validate that the workflow appears in the `Actions` tab of your repository

### Backend development track

In your GitHub Codespaces created in the previous step,

1. Add a new file named `dataverse-api-testing.yml` under the `.github/workflows` folder
2. Copy and paste the comment block below in the file created in the previous step

```yaml
# This is a GitHub Actions workflow for testing customizations implemented around Dataverse using its API.
# The workflow is triggered on every push only under the "src/Dataverse.API.Testing" folder to the main branch.
# This workflow need to define the following permissions to be able to publish the test results: permissions: contents: read, actions: read and checks: write
# 
# The workflow performs the following steps:
# 0. Enable git long paths: It enables long paths in git to avoid the "Filename too long" error.
# 1. Checkout repository: It checks out the repository to the runner, allowing the workflow to access the code.
# 2. Restore Dependencies: It restores all the dependencies and tools of the .NET solution (.sln) using the 'dotnet restore' command.
# 3. Build and Test: It targets a solution to runs API tests using the 'dotnet test' command leveraging the "TENANT_ID", "CLIENT_ID" and "CLIENT_SECRET" GitHub environment secrets and the "DATAVERSE_ENVIRONMENT_URL" environment variable. To be able to analyse the results of the tests, set also the following parameter: --logger "trx;LogFileName=test-results.trx"
# 4. Publish Test Results: Use the "dorny/test-reporter@main" GitHub action using the trx file generated by the test command and the "dotnet-trx" reporter to get a nice report of the test results. This step should always be run.
```

3. Press `Enter` and let you be guided by GitHub Copilot to complete the workflow file

> [!TIP]
> You can use the `Ctrl + i` keyboard shortcut combined with the `/doc` or `/explain` commands to explore how GitHub Copilot can help you even more in the context of this file.

> [!WARNING]
> GitHub Copilot suggestions are not always valid, so be careful and validate the code generated by it.
> We will use the time allocated to the next chapter and the fact that you will have some code to test to finalize the configuration of the GitHub workflow and make it work, don't worry.
> You can add the [`workflow_dispatch`](https://docs.github.com/en/actions/using-workflows/events-that-trigger-workflows#workflow_dispatch) event in the trigger section of the workflow to be able to run it manually and investigate why it is not working as expected.

4. Once the file is completed, commit and push the changes to the repository

5. Validate that the workflow appears in the `Actions` tab of your repository

[⬅️ Previous chapter](./04-OrganizeYourWork.md) | [🏡 Main agenda](../README.md#workshop-agenda) | [➡️ Next chapter](./06-CodeItAndShipIt.md)
