# LansweeperPasswordRecovery
Lansweeper 4, 5 & 6 Password Recovery Tool

For more information see our blog post: http://blog.gosecure.ca/2016/04/21/your-credentials-at-risk-with-lansweeper-5/

## Installation

- Open the C# project in Visual Studio 2015.
- Compile and enjoy.

## Usage

```
> LPR4and5.exe MVRtXmhzQXNpUzeu6JplTkcEqKyZ6K0vOdp/dakDvBT4YFC0vm52fr8YwiRnNZNxY7p2sk6IvM4mh6VCetFIpErgc2pzjvGxCg==
[*] Decrypting Lansweeper Password: MVRtXmhzQXNpUzeu6JplTkcEqKyZ6K0vOdp/dakDvBT4YFC0vm52fr8YwiRnNZNxY7p2sk6IvM4mh6VCetFIpErgc2pzjvGxCg==
[*] Note that this operation will NOT take a while...

[-] Recovered: private

...

> LPR6.exe Test_Encryption_File.txt Test_Ciphers_File.txt
[*] Processing files Test_Encryption_File.txt, Test_Ciphers_File.txt.
[*] Loading key file Test_Encryption_File.txt.
[*] Processing 1024 bytes for the key file.
[*] Loading cipher file test.txt
[*] Loading cipher line SNMP-Private:vX8OKLeWzlHtE/FMcCN0HqANgt6Y1083R1kz4sGeISU=
[*] Loading cipher line SNMP-Public:JKBl770rqkNnF7G7Y5uGKmyqBMT9r9sZo5VYIAg5d6I=
[-] Recovered password for user SNMP-Private as private
[-] Recovered password for user SNMP-Public as public
```
