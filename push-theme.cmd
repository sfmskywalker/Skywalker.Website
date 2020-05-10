SET branch="%1"
IF %branch%=="" SET branch="master"
echo %branch%
git subtree push --prefix=src/Themes/TheMediumTheme medium-theme %branch%