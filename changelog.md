# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [2.2.0] - 2022-02-06
### Added
- Added support to .NET5.0 and .ENT 6.0
### Fixed
- Bug for wrong serialization of Area in Country.
### Removed
- Removed support for .NET Core 2.1, 2.2 and 3.0

## [2.1.0] - 2020-02-26
### Added
- Added support to .NET Core 3.0 and 3.1.

## [2.0.1] - 2019-11-06
### Added
- Fixed exchange rates filter between dates.

## [2.0.0] - 2019-10-07
### Added
- Added support to .NET Core 2.2
- Implement new configuration for External APIs keys
### Changed
- Refactor of Services, remove ActionResponse dependency
- Improve Controller: Added Logging system, error management and ActionResponse management.

## [Previous versions < 2.0.0] - 2018-10-18
### Added
- Countries and cities parsing through embedded JSON file
- Currencies and exchange rates retrieving from external sources
