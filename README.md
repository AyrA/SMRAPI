# SMRAPIRI

**S**atisfactory **M**ap **R**epository **A**pplication **P**rogramming **I**nterface **R**eference **I**mplementation

## Purpose

This code serves as the reference implementation of the SMR API.

The code should make it easy for programmers to build their product on top of the SMR API.

## Safety

Being a reference implementation,
this code doesn't tries to catch all possible exceptions and validating all input.
It merely focuses on making the requests properly to demonstrate how it's done.

## Description of files

Below is the description of all important project files in alphabetical order

### `API.cs`

This file contains the API logic itself.
It makes requests on your behalf and decodes the XML answer

### `HTTP.cs`

This file demonstrates how to use a temporary local HTTP server to dynamically get the key from SMR.

### `Program.cs`

This is the file with the main entry point.
It has lots of commented code that shows how to use the API.

### `Responses.cs`

This file contains the class definitions for all possible API responses.

### `Tools.cs`

This file contains utility functions

## Help

The help for the API itsef can be found at https://cable.ayra.ch/satisfactory/maps/help/
