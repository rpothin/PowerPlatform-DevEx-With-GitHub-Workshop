# 04 - Organize your work

| **Goal**                                                                 | **Estimated duration** |
| ------------------------------------------------------------------------ | ---------------------- |
| Master the work organization capabilities offered by the GitHub platform | 15 min                 |

GitHub platform offers many capabilities to streamline the development process but also foster collaboration and transparency within teams.
By mastering these tools, teams can better manage their workflows, track progress, and deliver high-quality software more efficiently.

## Quick tour of the available capabilities

### [Issues & Projects](https://github.com/features/issues)

![Official image of a table view in GitHub Projects from Features website](https://github.githubassets.com/assets/memex-view-table-ab6c736cecef.png?width=1824&format=webpll)

![Official image of a board view in GitHub Projects from Features website](https://github.githubassets.com/assets/memex-view-board-cebeb9984e53.png?width=1824&format=webpll)

![Official image of a roadmap view in GitHub Projects from Features website](https://github.githubassets.com/assets/memex-view-roadmap-55ec09564df0.png?width=1824&format=webpll)

### [Discussions](https://github.com/features/discussions)

![Official image of GitHub Discussions from Features website](https://github.githubassets.com/assets/overview-d34a37d61239.png?width=1033&format=webpll)

## Hands-on excercice

### 1. Project setup

In your repository created from this GitHub repository template in the [chapter 3](./03-InitializeWorkspace.md) of this workshop,
1. Go to the `Projects` tab
2. Click on the downward-facing arrow at the right of the `Link a project` button
3. Select the `New project` option
4. Click on the `New project` button
5. Select one of the proposed template (_if you have no idea, you can choose the **Kanban** one which was presented earlier_)
6. In the `Project name` field, enter a meaningful value (_like "Power Platform GitHub DevEx Workshop"_)
7. Click on the `Create project` button

### 2. Initialize a milestone

In your repository created from this GitHub repository template in the [chapter 3](./03-InitializeWorkspace.md) of this workshop,
1. Go to the `Issues` tab
2. Click on `Milestones`
3. Click on `New milestone`
4. Enter a `Title`, a `Due date` (_you can set it to tomorrow for example_) and a short `Description` (_like "Power Platform GitHub DevEx Workshop excercice"_)
5. Click on the `Create milestone` button

### 3. Create a feature request using the provided form

In your repository created from this GitHub repository template in the [chapter 3](./03-InitializeWorkspace.md) of this workshop,
1. Go to the `Issues` tab
2. Click on the `New issue` button
3. On the `Feature Request (form)` line, click on the `Get started` button
4. Enter the requested information: Title, Description and Priority
5. Check the `Code of conduct` box
6. Click on the `Submit new issue` button
7. Add the new issue to the project and the milestone created in the previous step by using the related sections in the right panel

### 4. Create an issue form for bugs

In your repository created from this GitHub repository template in the [chapter 3](./03-InitializeWorkspace.md) of this workshop, from the **Code** tab,
1. Press the `.` key of your keyboard for a few seconds to open the [github.dev web-based editor](https://docs.github.com/en/codespaces/the-githubdev-web-based-editor)
2. Under [.github/ISSUE_TEMPLATE/](../.github/ISSUE_TEMPLATE/) directory, create new `3-bug.yml` file
3. Enter the following code into the new file

```yml
name: Bug Report
description: File a bug report
title: "[Bug] "
labels: ["bug"]
projects: []
assignees: []
body:
```

4. Then add a few elements to the form based on the following example from the GitHub documentation: [Creating issue forms](https://docs.github.com/en/communities/using-templates-to-encourage-useful-issues-and-pull-requests/configuring-issue-templates-for-your-repository#creating-issue-forms)
5. Save your file
6. In the `Source Control` tab of your editor, stage your new file
7. Add a commit message like "Add bug issue form"
8. Commit the change
9. Go back to your repository and validate your new bug issue form is available and ready to use

[⬅️ Previous chapter](./03-InitializeWorkspace.md) | [🏡 Main agenda](../README.md#workshop-agenda) | [➡️ Next chapter](./05-SomeALMSetup.md)