# Slack User Change Alerts

An Azure Function that sends a Slackbot notification to specified users or channels when users are added or removed from Slack.

Ported from https://github.com/cfe84/slack-users-change-alerts.

## Quick Deploy to Azure

[![Deploy to Azure](http://azuredeploy.net/deploybutton.svg)](https://azuredeploy.net/)

See below for application setting values.

## Application settings

Here are the app settings that the function app depends on:

- **SlackApiToken** - The token used to query the Slack API. Obtained from [https://api.slack.com/custom-integrations/legacy-tokens](https://api.slack.com/custom-integrations/legacy-tokens)
- **SlackbotUrl** - URL to the webhook for invoking Slackbot. Obtained from Slack by going to: `'Apps and Integrations' > 'Apps' > 'Custom Integrations' > 'Slackbot'`. Create a new configuration if necessary.
- **ChannelsToNotify** - Comma-separated list of users or channels to notify (include @ or #). For example: `@anthony, @charles`
- **StorageConnection** - Connection string to a Storage account. Used to persist state between function executions.

## Running Locally

Visual Studio function app project is included. To run locally, create an `appsettings.json` file in the root of the function app. There is a sample included.
