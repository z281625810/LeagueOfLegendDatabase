Create Table ChampionBasic
(
	chName			varchar(20),
	chDescription	varchar(50),
	chGender		char(1),
	Primary Key (chName)
)

Create Table ChampionStats
(
	chName			varchar(20),
	chHealth		int,
	chAttack		int,
	chRange			int,
	Primary Key (chName)
)

Create Table Skills
(
	skName			varchar(30),
	skType			varchar(20),
	skDamage		int,
	Primary Key (skName)
)

Create Table HaveSkillOf
(
	chName			varchar(20),
	skName			varchar(30),
	skillNum		int,
	Primary Key (chName, skName),
	Foreign Key (chName) References ChampionBasic (chName),
	Foreign Key (skName) References Skills (skName)
)

Create Table ChampType
(
	tID				int,
	tName			varchar(10),
	Primary Key (tID)
)

Create Table ChampTypeOf
(
	chName			varchar(20),
	tID				int,
	Primary Key (chName, tID),
	Foreign Key (chName) References ChampionBasic (chName),
	Foreign Key (tID) References ChampType (tID)
)

Create Table Equipment
(
	eqName			varchar(40),
	eqDescription	varchar(400),
	eqStat			varchar(300),
	Primary Key (eqName)
)

Create Table EquipType
(
	eqID			int,
	eName			varchar(30),
	Primary Key (eqID)
)

Create Table EquipTypeOf
(
	eqName			varchar(40),
	eqID			int,
	Primary Key (eqName, eqID),
	Foreign Key (eqName) References Equipment (eqName),
	Foreign Key (eqID) References EquipType (eqID)
)

Create Table Equipped
(
	chName			varchar(20),
	eqName			varchar(40),
	Primary Key (chName, eqName),
	Foreign Key (chName) References ChampionStats (chName),
	Foreign Key (eqName) References Equipment (eqName)
)