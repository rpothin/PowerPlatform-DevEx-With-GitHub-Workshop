# 07 - Job's not finished!

| **Goal**                                                                                                                                                                                 | **Estimated duration** |
| ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------- |
| Understand that writing code is a part of our mission of building applications but we still need to be conscious about important things, like Security, and GitHub can help us with that | 30 min                 |

![Kobe Bryant - Job's not finished](https://i.pinimg.com/originals/a4/b9/aa/a4b9aa5acd5bcd4c8c508db4bb48d5c1.jpg)

## Scanning capabilities

### [Code scanning](https://docs.github.com/en/code-security/code-scanning/introduction-to-code-scanning/about-code-scanning)

> [!NOTE]
> Not available in the repository while its visibility is [private](https://docs.github.com/en/code-security/code-scanning/introduction-to-code-scanning/about-code-scanning#about-billing-for-code-scanning). I will need to wait the last days to finalize this section.
> I will try first to let GitHub try to setup the code scanning related to the code under `resources/` for me.
> If it doesn't work, I will try to do it manually.

### [Secret scanning](https://docs.github.com/en/code-security/secret-scanning/about-secret-scanning#about-secret-scanning)

In your repository created from this GitHub repository template in the [chapter 3](./03-InitializeWorkspace.md) of this workshop, follow the steps below to verify if the GitHub Secret Scanning capability is enabled and to test it:
1. Go to the `Settings` tab of your repository
2. Under the `Security` section, click on the `Code security and analysis` menu
3. Go to the bottom of the page and validate that under `Secret scanning` the following options are enabled: `Receive alerts on GitHub for detected secrets, keys, or other tokens.` and `Push protection`
4. In your GitHub Codespace, create a new file named `test.json` and add the following content where you replace the value of the `key` property with a **real** secret value related to an existing app registration that will be provided by the workshop host:
   ```json
   {
     "key": ""
   }
   ```
5. Create a commit, and in your terminal run the following command to try to push the changes to your repository:
   ```bash
   git push
   ```
6. You should see an error message related to the secret value you added in the `test.json` file

> [!CAUTION]
> If you use the link provided in the error message to bypass the secret scanning and run again the push command, you will be able to push the changes to your repository.
> **Do not do that!** except if you are really sure to know what you are doing. The secret will stay "exposed" in the secret scanning logs of your repository.

## Dependencies management

### [Dependency graph](https://docs.github.com/en/code-security/supply-chain-security/understanding-your-software-supply-chain/about-the-dependency-graph)

![image](https://github.com/rpothin/PowerPlatform-DevEx-With-GitHub-Workshop/assets/23240245/a75c1286-3022-4cbc-8a61-befff0a3e54e)

In your repository created from this GitHub repository template in the [chapter 3](./03-InitializeWorkspace.md) of this workshop, follow the steps below to access the Dependency graph:
1. Go to the `Insights` tab of your repository
2. Click on `Dependency graph`
3. Click on the `Enable the dependency graph` button
4. Refresh the page
5. You should see the list of dependencies used in your repository

### [Dependabot](https://docs.github.com/en/code-security/getting-started/dependabot-quickstart-guide)

#### [Dependabot alerts](https://docs.github.com/en/code-security/dependabot/dependabot-alerts/about-dependabot-alerts#github-dependabot-alerts-for-vulnerable-dependencies) & [Dependabot security updates](https://docs.github.com/en/code-security/dependabot/dependabot-security-updates/configuring-dependabot-security-updates)

These amazing security capabilities are enabled by default in public repositories and can easily be enabled in private repositories.

To check the status of these capabilities in your repository created from this GitHub repository template in the [chapter 3](./03-InitializeWorkspace.md) of this workshop, follow the steps below:
1. Go to the `Settings` tab of your repository
2. Under the `Security` section, click on the `Code security and analysis` menu
3. Verify that the following options are enabled: `Dependency graph`, `Dependabot alerts` and `Dependabot security updates`

#### [Dependabot version updates](https://docs.github.com/en/code-security/dependabot/dependabot-version-updates/configuring-dependabot-version-updates)

> [!NOTE]
> This capability is not enabled by default in repositories. You need to enable it manually by configuring a `.github/dependabot.yml` file in your repository.
> A sample configuration file is available in the `.github` folder of this repository template.
> For the workshop, we will see if there are pull requests created by Dependabot we could look at together.

> [!NOTE]
> Around dependencies management, there is also the following capability available in GitHub: [Reviewing dependency changes in a pull request](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/reviewing-changes-in-pull-requests/reviewing-dependency-changes-in-a-pull-request)

[⬅️ Previous chapter](./06-CodeItAndShipIt.md) | [🏡 Main agenda](../README.md#workshop-agenda)
