Itsy-Bitsy Level Generator

It is an input based dynamic level generator using a seed (string input) then converted to hash then to binary.
The binary data is then processed and read by the level generator and outputs a designed level.

input/seed = helloworld
hash = fc5e038d38a57032085441e7fe7010b0
binary = 01100110 01100011 00110101 01100101 00110000 00110011 
	 00111000 01100100 00110011 00111000 01100001 00110101
	 00110111 00110000 00110011 00110010 00110000 00111000 
	 00110101 00110100 00110100 00110001 01100101 00110111 
	 01100110 01100101 00110111 00110000 00110001 00110000 
	 01100010 00110000

These data can be used to create various kinds of level depending on the interpretation.

Simple Sides Scrolling Platformer Game Intepretation:

Represent 1 = _ and 0 = ' '

011001100110001100110101011001010011000000110011001110000110010000110011001110000110000100110101

 __  __  __   __  __ _ _ __  _ _  __      __  __  ___    __  _    __  __  ___    __    _  __ _ _  
	
This representation can now be used as the level platform / ground with spaces that the player must jump across.

This tool eliminate saving levels in games as it is dynamically loaded given a seed.  
The same seed would always generate the same level.
