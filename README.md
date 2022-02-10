# The Medium Theme

An Orchard Core theme based on the [Mediumish](https://www.themepush.com/freethemes/mediumish/index.html) theme from [Wow Themes](https://www.wowthemes.net/).

Demo: https://www.youtube.com/watch?v=OI_TmVE3Rj0

## Features

* Blog
	- "Read time" liquid filter (e.g. "6 min read")
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
* More configurability options (e.g. footer copy right notice)
* Forms (e.g. contact form)
* Widgets & Flows (currently everything is liquid-based using LiquidPage)
* Tag cloud

# Infrastructure

* Pulumi:
    - Azure App Service Plan
    - Azure App Service
    - Azure Storage Account
    - Azure SQL Server
    - Azure Container Registry
    - Push secrets to GitHub (for GitHub Actions)
* GitHub Actions to build & push Docker container

Test
