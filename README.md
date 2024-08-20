### Undersoft Suite 
#### Undersoft Software Development Kit
#### Shared Service Center - Shared Vaccination Center - Shared Contact Center - Global Currency Converter

Open source conceptual resources to develop distributed, scalable, multi tenant web application in N-Tier architecture. Front tier with single code base hybrid web assembly, android, windows clients. Application tier with application server. Service tier with administration server and operational data services. Data tier with database stores (available: npgsql, mysql, mariadb, sqlserver, oracle, mongo, sqlite, cosmodb, azuresql, inmemorydb) and blob/file storages.       

#### Latest Add:
#### Plot: 
Specific type of graph. Plot can contain many crossed path lines. Paths can start from same or different source and ends on same or different target. Time is set as default kind of metric value. Below simple example where diagonal value is presented as max value from x, y correlated to time of movement on x and y started at the same time where distance of x, y is constant and can equal for example 1 meter for road map plot or 100 micrometers for high precision plot. By defining metric ranges, nodes can be excluded from computing and decrease complexity. For example ranges (from 0 to 1) will take only 0 (let's name it pedestrian crossing) and 1 (let's name it road) this ranges will compute path for cars on road. When ranges (from 2 to 2) where 2 (let's name it walk) and (from 0 to 0) will be defined result will compute path for walk road. Example source-target pairs {source:[x,y], target:[x,y]} first path (source:[Y,3], target:[B,3]}. Second {source:[P,0], target:[X,7]}. Third {source:[Y,3], target:[X,7]}.

<code> 
yx ABCDEFGHIJKLMNOPRSTUWVXY
0  888888888888821111288888
1  333333333333320000288888
2  222222222222220000222222
3  211111111111101111011111
4  222222222222220000222222
5  888888888888821111288888
6  222222222222220000222222
7  111111111111101111011112
8  222222222222220000222222
9  888888888888821111288888
</code>

#### Plot.QuickPath: 
ShortestPathDijkstra algorithm modification by replacing UpdatePriority to TryDequeue and Enqueue operation. UpdatePriority is not available in .NET 8, System.Collections.Generic.PriorityQueue. [https://github.com/undersoft-org/Undersoft.Suite/blob/07c9d385d8d969374991094c972859fa7c5d1b2d/src/SoftwareDevelopmentKit/src/Undersoft.SDK/Series/Complex/Plot/Plot.cs#L139](https://github.com/undersoft-org/Undersoft.Suite/blob/07c9d385d8d969374991094c972859fa7c5d1b2d/src/SoftwareDevelopmentKit/src/Undersoft.SDK/Series/Complex/Plot/Plot.cs#L139
First Tests: 
[https://github.com/undersoft-org/Undersoft.Suite/blob/5053c6de6b45cdc8b7f502e8f2acdd5f6618e689/src/SoftwareDevelopmentKit/tests/Undersoft.SDK.Tests/Series/Complex/PlotTest.cs](https://github.com/undersoft-org/Undersoft.Suite/blob/5053c6de6b45cdc8b7f502e8f2acdd5f6618e689/src/SoftwareDevelopmentKit/tests/Undersoft.SDK.Tests/Series/Complex/PlotTest.cs)

#### SDK Benchmarks: 
[https://github.com/undersoft-org/Undersoft.Suite/tree/master/src/SoftwareDevelopmentKit/bench/Undersoft.SDK.Benchmarks](https://github.com/undersoft-org/Undersoft.Suite/tree/master/src/SoftwareDevelopmentKit/bench/Undersoft.SDK.Benchmarks)

#### Design overview screenshots: 
[https://github.com/undersoft-org/undersoft-org/Undersoft.Suite/src/SharedVaccinationCenter/docs/design/vaccination_software_design_album](https://github.com/undersoft-org/Undersoft.Suite/tree/master/src/SharedVaccinationCenter/docs/design/vaccination_software_design_album/solution)


### Landing
![SSC_Landing](https://github.com/undersoft-org/Undersoft/assets/82622935/9273f4b7-7c83-42c0-a4af-7f56a7f0dc44)

### Guest 
![publiclayout](https://github.com/user-attachments/assets/0c4d3434-4491-4d0f-8a4f-0fd1d879b7d3)

### Admin
![lightadminlayout](https://github.com/user-attachments/assets/9cab7d8a-0e36-4507-8649-51d6771b73a6)

### User
![userlayout](https://github.com/user-attachments/assets/4b36bfad-c880-4ab9-9252-98977a042a46)

### Microservices
![microservices](https://github.com/user-attachments/assets/ec1451a6-13c6-4ef2-bbd1-e2b20d312a6b)

### Open Search
![winhybridinventory](https://github.com/user-attachments/assets/cc349ba0-b2da-4efb-ad15-f9041422d136)

### Open Filtering
![winhybridvaccination](https://github.com/user-attachments/assets/46b961b4-21be-4257-9f62-97cf6f612459)

### Web Assembly UI - Single Base Code
![webassemblyclient](https://github.com/user-attachments/assets/8fb72f76-e9cc-46b1-a308-4ddaa4e98683)

### Windows Hybrid UI - Single Base Code
![winhybridcatalogs](https://github.com/user-attachments/assets/ee514064-53f6-4e6d-bb64-c491caef19ab)

### Android Hybrid UI - Single Base Code
<img src="https://github.com/user-attachments/assets/b6db8ecf-b360-444e-a43f-467d03111e35" width=18% height=20% /> 

<img src="https://github.com/user-attachments/assets/07ba4dbe-fb6a-46b4-b084-5aa83f222ec5" width=18% height=20% /> 

<img src="https://github.com/user-attachments/assets/0827b5c8-ffd1-4c82-b843-c75535a27ad4" width=18% height=20% /> 

<img src="https://github.com/user-attachments/assets/87a3eca5-7bef-46ed-a381-b8224dd8318d" width=18% height=20% /> 

<img src="https://github.com/user-attachments/assets/635bee8c-a872-4701-ac3c-140acef27e95" width=18% height=20% /> 

### Email confirmation
![emailconfirmationpanel](https://github.com/user-attachments/assets/65b83061-11cc-471a-957e-bca72f4250d4)

### Context Menus 
![light_menus](https://github.com/user-attachments/assets/27fe10bd-17ce-44f0-94cc-02bb08587592)

### Consents
![consents](https://github.com/user-attachments/assets/04b58a52-7832-4262-9d09-16a4f8e7576f)

### Wizards
![registrationwizard](https://github.com/user-attachments/assets/42b6951c-44ee-4e66-a286-a0a4422896b0)

### Validation
![stepvalidation](https://github.com/user-attachments/assets/25ee17db-f9a9-401b-a1e8-f4b091b4079d)

### Recovery verification
![recoveryverification](https://github.com/user-attachments/assets/a7caaa10-68a7-40cd-93f4-112d338f33f2)

### Custom Service Center
![SSC_1](https://github.com/undersoft-org/Undersoft/assets/82622935/7b65b354-5a0c-4b50-8e2c-9b04b9dbe549)

### Acounts
![SSC_Accounts](https://github.com/undersoft-org/Undersoft/assets/82622935/3def196a-c93b-4797-acd2-cb2a0f4d8d1d)

![SSC_up](https://github.com/undersoft-org/Undersoft/assets/82622935/28a36285-f8af-424e-9c09-ccce29b457e8)

![SSC_in](https://github.com/undersoft-org/Undersoft/assets/82622935/75774832-a5be-4556-b750-96e188660f47)


