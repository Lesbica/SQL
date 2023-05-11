create table Visitor
(
    ID int not null primary key,
    FullName varchar(100) not null,
    Age int check(Age > 0 and Age < 130) not null,
    Allergies varchar(100) not null,
    Contacts varchar(10) not null
);

create table Orders
(
    OrderNum int not null primary key,
    foreign key (Visitors) references Visitor(ID),
    Priority int check(Priority > 0 and Priority < 3) not null,
    DueDate date not null,
    TotalPrice double check(TotalPrice > 0.01),
    Visitors int not null
);

/*create table AnalysisType
(
    CodeAnalType int not null primary key,
    NameAnalType varchar(100) not null,
    DescriptionAnalType varchar(100) not null
);*/

/*create table Analysis
(
    AnalCode int not null primary key,
    foreign key (AnalysisType) references AnalysisType(CodeAnalType),
    AnalysisType int not null,
    Price double not null
);*/

/*create table LabAssistant
(
    ID int not null primary key,
    FullName varchar(100) not null,
    Specialization varchar(100) not null
);*/

/*create table Result
(
    CodeResult int not null primary key,
    ShortInfo varchar(100) not null,
    LongInfo text not null
);*/

/*create table AnalysisOrders
(
    foreign key (OrderNum) references Orders (OrderNum),
    foreign key (AnalCode) references Analysis (AnalCode),
    foreign key (Result) references Result (CodeResult),
    DateOfResults date not null,
    OrderNum      int  not null,
    Result        int  not null,
    AnalCode int not null,
    foreign key (LabAssistant) references LabAssistant(ID),
    LabAssistant int not null
);*/

/*create table LaborantAnalysis
(
    AnalCode     int not null,
    LabAssistant int not null,
    foreign key (AnalCode) references Analysis (AnalCode),
    foreign key (LabAssistant) references LabAssistant (ID)
);*/

/*create table Reagent
(
    ReagCode int not null primary key,
    NameReagent varchar(100) not null,
    DescriptionReagent varchar(100) not null,
    Available bool
);*/

/*create table ReagentsInAnalysis
(
    foreign key (AnalCode) references Analysis(AnalCode),
    foreign key (ReagCode) references Reagent(ReagCode),
    UsedCount int not null,
    AnalCode int not null,
    ReagCode int not null
);*/
