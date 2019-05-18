Build status
[![Build Status](https://dev.azure.com/OpenRiaServices/OpenRiaServices/_apis/build/status/OpenRIAServices.OpenRiaServices.M2M?branchName=master)](https://dev.azure.com/OpenRiaServices/OpenRiaServices/_build/latest?definitionId=2&branchName=master)

Sonarcloud status
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=OpenRIAServices_OpenRiaServices.M2M&metric=alert_status)](https://sonarcloud.io/dashboard?id=OpenRIAServices_OpenRiaServices.M2M)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=OpenRIAServices_OpenRiaServices.M2M&metric=security_rating)](https://sonarcloud.io/dashboard?id=OpenRIAServices_OpenRiaServices.M2M)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=OpenRIAServices_OpenRiaServices.M2M&metric=sqale_rating)](https://sonarcloud.io/dashboard?id=OpenRIAServices_OpenRiaServices.M2M)
[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=OpenRIAServices_OpenRiaServices.M2M&metric=reliability_rating)](https://sonarcloud.io/dashboard?id=OpenRIAServices_OpenRiaServices.M2M)
[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=OpenRIAServices_OpenRiaServices.M2M&metric=bugs)](https://sonarcloud.io/dashboard?id=OpenRIAServices_OpenRiaServices.M2M)



## Overview

M2M4RIA is an extension for [OpenRIAServices](https://github.com/OpenRIAServices/OpenRiaServices) that adds support for many-2-many relations.

## Features

* Can be installed with NuGet.
* Supports Entity Framework code-first, model first, and DbContext.
* A strongly-typed configuration mechanism using the [Fluent Metadata API](https://riaservicescontrib.codeplex.com/wikipage?title=FluentMetadata%20for%20WCF%20RIA%20Services) for OpenRIAServices
* A code generator that seamlessly integrates with the code generator of OpenRIAServices.
* Requires only a minimal adaption of your data model (see StepByStepInstructions on wiki).
* Supports many-2-many relations with composite keys.

## Download

M2M4RIA is distributed as a collection of NuGet packages:
* [OpenRIAServices.M2M](https://nuget.org/packages/OpenRiaServices.M2M) This is the server-side part of M2M4RIA. It contains an entity code generator and an extension to the fluent metadata configuration for OpenRIAServices.
* [OpenRIAServices.M2M.LinkTable](https://nuget.org/packages/OpenRIAServices.M2M.LinkTable) This is a generic link table implementation that is used for creating "link table" views for your M2M relations (see [GeneralOverview](../GeneralOverview)).
* [OpenRIAServices.M2M.Silverlight](https://nuget.org/packages/OpenRIAServices.M2M.Silverlight) This is the client-side part of M2M4RIA. It contains classes for creating M2M views (see [GeneralOverview](../GeneralOverview)).

## Usage

1. Add the [M2M4RIA NuGet](https://nuget.org/packages?q=OpenRiaServicesM2M) packages to your solution.
2. Create LinkTable entities (By subclassing a generic LinkTable class provided by M2M4RIA).
3. Extend your data model with "link table" views.
4. Configure your M2M relations using the fluent metadata API.
5. Add Insert/Delete operations to your domain service for your link table entities 
A complete step-by-step guide is provided [here](https://github.com/OpenRIAServices/OpenRiaServices.M2M/wiki/Step-by-step-instructions-for-using-M2M-with-OpenRIAServices).

## Demo

The source code repository contains a sample application that shows how M2M4RIA can be used..

## See Also
For more information and installation instructions go to https://github.com/OpenRIAServices/OpenRiaServices.M2M and check the wiki

Step by step guide is availible at the wiki
