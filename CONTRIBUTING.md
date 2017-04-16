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

### Item Serialization

The project uses [Unicorn](https://github.com/kamsar/Unicorn) to serialize Sitecore items related to Conjunction, i.e template items, settings etc. 

To get the item serialization working, you must perform the following steps:

1. Publish the ``Conjunction.Foundation.Serialization`` and ``Conjunction.Foundation.Core`` projects, using a custom filesystem publish profile, to your local Sitecore instance (IIS webroot folder)
2. In the local Sitecore instance, locate the file ``.\Website\App_Config\Include\Conjunction\Conjunction.Foundation.Serialization.DevSettings.config.example`` and remove the ``.example`` extension
3. Edit the ``Conjunction.Foundation.Serialization.DevSettings.config`` file and set the ``sourceFolder`` variable to point to where the Conjunction source code is located on your machine (the working directory)
4. Run ``http://<yoursite>/unicorn.aspx`` and sync everything

### Tests

Tests are located in the ``*.Tests`` projects and are using [xUnit.net](https://xunit.github.io/). By default, the test projects use [Visual Studio runner](https://github.com/xunit/visualstudio.xunit) to run the tests.

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

The docs will be served at [http://localhost:4000](http://localhost:4000).

#### Publishing the Docs

To publish the documentation, run the following:

```
npm run docs:publish
```

### Sending a Pull Request

In general, the contribution workflow looks like this:

* Open a new issue in the [issue tracker](https://github.com/soen/Conjunction/issues).
* Fork the repo.
* Create a new feature branch based off the master branch.
* Make sure all tests pass.
* Submit a pull request, referencing any issues it addresses.

Please try to keep your pull request focused in scope and avoid including unrelated commits.

After you have submitted your pull request, I'll get back to you as soon as possible.

Thank you for contributing!