# Contributing

When contributing to this repository, please first discuss the change you wish to make via issue before making a change. 

Please note there is a [code of conduct](CODE_OF_CONDUCT.md), please follow it in all your interactions with the project.

## Reporting Issues and Asking Questions

Before opening an issue, please search the [issue tracker](https://github.com/soen/Conjunction/issues) to make sure your issue hasn't already been reported.

## Development

Visit the [issue tracker](https://github.com/soen/Conjunction/issues) to find a list of open issues that need attention.

Fork, then clone the repo:

```
git clone https://github.com/your-username/conjunction.git
```

### Tests

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

### Sending a Pull Request
TODO...