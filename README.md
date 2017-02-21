# Slack User Change Alerts

An Azure Function that sends a Slackbot notification to specified users or channels when users are added or removed from Slack.

Port from https://github.com/cfe84/slack-users-change-alerts.

## Getting Started

- Fork this repo
- Create a new Function App in Azure
- Add these application settings to the Function App:
    - SlackApiToken - The token used to query the Slack API
    - SlackbotUrl - URL to the webhook for invoking Slackbot
    - StorageConnection - Connection string to a Storage account. Used to persist state between function executions.
    - ChannelsToNotify - Comma-separated list of users or channels to notify (include @ or #). For example: `@anthony, @charles`
- Set up a continuous deployment from the forked repo in GitHub
- Watch the notifications come in

## Running Locally

Visual Studio function app project is included. To run locally, create an `appsettings.json` file in the root of the function app. There is a sample included.
