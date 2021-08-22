# DarkBattle
This repository is for my project assignment - Web application.

DarkBattle is a browser game, that take place in fantasy dark ages. It is RPG (role playing game) with 3 classes:
* Warrior
* Archer
* Mage
* Rouge

Each class has it own strength and weakness.
Game will have different areas with requirements of minimum level for champions.
Each area ends with "dungeon", where you can test your champion and win big reward.


App development state:

Controller conception:
* Area
* Creature
* Item
* ChampionClasse
* Merchant
* Consumble

Progress :
* Area controller with edit, create and index view
* Creature controller with edit, create and index view
* Item controller with edit, create and index view
* Consumble controller with edit, create and index view
* ChampionClass controller with edit, create and index view
* Merchant controller with edit, create and index view
* Project split in different libraries:
	* Data library - contains all information about database content, connection and models;
	* Service library - all business logic	and mapping for automapper;
	* View model - contains models for views;
	* Data constans - contains constans used in applicaion;
	* Test - library for all test used to verify bussines logic;
	* Seeder - library with initial object add to application; 




	
