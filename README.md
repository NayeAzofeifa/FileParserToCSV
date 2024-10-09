# FileParserToCSV

## Description
FileParserToCSV is a console program that can create a CSV file parsing the data from an input text file.

## Features
- **File reading**: Read text data file "InputData.txt" from the "Incoming" directory.
- **CSV File Generation**: Create files for each customer in the input file, includes a header, customer information, and detail records in the "Outgoing" directory.
- **File Backup**: Copies all input and generaded files (InputData.txt and the CSV file generated for each customer) to a backup directory with a "_Backup" suffix in the "Backup" directory.

## Project Structure
- Incoming: Folder where "InputData.txt" is stored.
- Outgoing: Folder where generated CSV files are created.
- Backup: Folder where backup copies of the input and generated files are stored
- Models: Contains model classes representing the data (Customer information, Header records, Details records).
- Services:  Contains service classes for business logic (Files services: Handles file operations like reading and writing and Calculation Service: Handles various logic and calculations related to customer data).

## Requirements
- .NET 7.0 or higher
- A compatible development environment (IDE) such as Visual Studio or Visual Studio Code
