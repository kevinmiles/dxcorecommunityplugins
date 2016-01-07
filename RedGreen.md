RedGreen is a plug-in that will allow you to launch your unit tests from within Visual Studio. Unlike other tools that do this, it will also parse the test results and overlay them on your test.

# Introduction #

The goal of red green is to give you tigher integration between your tests and the results of running them. Using RedGreen you can launch the tests from Visual Studio and get color coded results right on top of the editor window. RedGreen also parses errors and does its best to overlay the failure data after the failing assert.

RedGreen also allows you to run methods with no parameters as though they were tests even if they are not decorated with a test attribute. Using this feature you can try out programming ideas and see what happens. Most of the time you simply write your results to the console and that will be echoed to the Visual Studio Output pane.

# Installation #

Two steps are needed for RedGreen to work. The first is to follow the normal DxCore Community Plug-in instructions. The second is to install Gallio, available at www.gallio.org. RedGreen uses Gallio to run the tests because it supports most of the current testing frameworks.

# Usage #

Simply hover over the tets runner Icon and select a "Run" option.

# Options #

TODO

# Credits #

Author: Jim Argeropoulos