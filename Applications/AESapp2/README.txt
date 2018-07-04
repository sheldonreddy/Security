Features:
	1. GUI to demo the Encryption and Decryption.
	
		There is an option for using a session-generated KEY and IV parameter
			This will change after every session ( every time the code is run )
		
		There is also the option of using a Persistent KEY and IV parameter.
			Simply DEFINE the "USEPERSISTENTKEY" in Program.CS on line 21 or thereabout
	
	2. Implementation of the SecureTextBox.
			This allows masking of password when being entered.
			This allows for the storage of an encrypted password and not a plaintext password. - very safe!
	
	3. TEST SAMPLE Assembly .dll or Class Library file for use in MATLAB, VS for using functionality.
		
		The MATLAB script and .dll file in the project folder ../AESapp2 will allow you to test this
			MATLAB script: mlCrypto.m
			Assembly file: cryptoAES.dll
	
	4. MODIFY AND CREATE YOUR OWN Assembly .dll or Class Library file for use in MATLAB, VS for using functionality.
	
		To use this, you must first build the project with an outputType as Class Library.
		Program.CS has documentation to show you how to do this. 
		Once completed, copy the cryptoAES.dll file from the following location:
		
					...\AESapp2\crypto\obj\Debug
	
		Usage (Adapt to your environment of choice - this is for MATLAB):
			
			MATLAB script: mlCrypto.m provided in ../AESapp2/ to play with
			Ensure you replace the cryptoAES.dll in ../AESapp2/ when you modify and build your own.
			
			a. LOAD cryptoAES.dll into your project
									
				NET.addAssembly('...\cryptoAES.dll');		<- THIS IS FOR MATLAB, CHANGE FOR YOUR USAGE
				
			b. Launch GUI
			
					cryptoAES.AES.Main();
				
			c. Get Username (returns String)
			
					username = cryptoAES.AES.getUsername()
			
			d. Get Encrypted Password (returns Byte [] )
			
					encryptedPassword = cryptoAES.AES.getEncryptedPassword()
			
			e. Get Decrypted Password using (3)
			
					password = cryptoAES.AES.getDecryptedPassword(encryptedPassword)
			
			f. Cleanup
			
					cryptoAES.AES.cleanup();
					
					
	
