# KNNTextAnalysis

This is the project implementing KNN analysis of [Reuters-21578](http://archive.ics.uci.edu/ml/datasets/Reuters-21578+Text+Categorization+Collection) text categorization dataset.

## How to start

Dependencies:

- Visual Studio 2022
- .NET 6.0
- 7z
- PowerShell 7

1. Assuming, you opened the terminal in the project folder `KNNTextAnalysis`.
2. Start the `Datasets\Get-Datasets.ps1` script by typing:

    ```PowerShell
    cd Datasets
    .\Get-Datasets.ps1
    ```

    This will download and extract `reuters-21578.tar.gz` archive, which contains the dataset.

3. Start the Visual Studio 2022.
4. For better perfomance, change the build option to Release Mode
5. Run the project.

    The project writes console output to `Datasets/log-{timestamp}.txt`, where you can collect the Accuracy, Precision, Recall for different scenarios.
