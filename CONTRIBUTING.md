# Contributing

When contributing to this repository, please first discuss the change you wish to make via issue before making a change. 

Please note there is a code of conduct, please follow it in all your interactions with the project.

## Pull Request Process

1. Ensure any install or build dependencies are removed before the end of the layer when doing a build.
2. You may merge the Pull Request in once you have the sign-off of at least one other project maintainer, that has reviewed the changes you want to merge.

## Development

Visit the issue tracker to find a list of open issues that need attention.

Fork, then clone the repo:

```
git clone https://github.com/your-username/conjunction.git
```

### Docs

Improvements to the documentation are always welcome. In the docs we abide by typographic rules, so instead of ' you should use '. Same goes for “ ” and dashes (—) where appropriate. These rules only apply to the text, not to code blocks.

#### Installing Gitbook

To install the latest version of `gitbook` and prepare to build the documentation, run the following:

```
npm run docs:prepare
```

#### Building the Docs

To build the documentation, run the following:

```
npm run docs:build
```

To watch and rebuild documentation when changes occur, run the following:

```
npm run docs:watch
```

The docs will be served at http://localhost:4000.

#### Publishing the Docs

To publish the documentation, run the following:

```
npm run docs:publish
```