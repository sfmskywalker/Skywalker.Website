# TheMediumTheme

An Orchard Core theme based on the Mediumish theme from Wow Themes.

## Features

* Blog
	- "Read time" liquid filter
* Tags
* Search
* Site Logo
* Author extends User
	- Name
	- Tagline
	- Bio
	- Social URLs

## Roadmap

* Comments
* More configurability options (e.g. footer copy right, )
* Forms (e.g. contact form)
* Widgets & Flows (right now everything is liquid-based using LiquidPage)
* Tag Cloud

# Infrastructure

* Pulumi to stand up:
    - Azure App Service Plan
    - Azure App Service
    - Azure Storage Account
    - Azure SQL Server
    - Azure Container Registry
    - Push secrets to GitHub (for GitHub Actions)
* GitHub Actions to build & push Docker container