% Author: 		Sheldon Reddy
% Description:	Test Assembly(.dll) interface for AES256 

%% Load Assembly file
% Add the path to the file "cryptoAES.dll" found in the ../cryptoAES/ directory
asm = NET.addAssembly('	 ENTER LOCATION OF THE DLL HERE	 - USE FULL FILE PATH	');

%% Run the login form
cryptoAES.AES.Main();

%% Get Username
username = cryptoAES.AES.getUsername()

%% Get Encrypted Password ( System.Byte [] )
encryptedPassword = cryptoAES.AES.getEncryptedPassword()

%% Get Decrypted Password by passing Encrypted Password as Parameter
% Commented out to test use case for separate sessions
% password = NedbankAES.AES.getDecryptedPassword(encryptedPassword)

%% Store Encrypted Password to MAT file

% Map System.String to MATLAB equivalent ( uint8 Scalar )
mlPassword = uint8(encryptedPassword);
filename = 'password.mat';
save(filename,'mlPassword');

%% Load Encrypted Password from MAT file

% Clear workspace and load Assembly file to emulate New Session
% You can also close and re-open Matlab to and run from this point if you're the anal type 
clear
clc
% Add the path to the file "cryptoAES.dll" found in the ../cryptoAES/ directory
asm = NET.addAssembly('	 ENTER LOCATION OF THE DLL HERE	 - USE FULL FILE PATH	');

% Load Password from MATLAB
load('password.mat')

% Ensure the password isn't readable in Matlab and is still encrypted
char(mlPassword)

% Get Decrypted Password by passing Encrypted ML Password as Parameter
password = cryptoAES.AES.getDecryptedPassword(mlPassword)


%% Clean memory location used by dll to store plainText password.
cryptoAES.AES.cleanup();