Task description

 Find optimal job assignment strategy
 
 AGVs are fulfilling given tasks (taking material from place A to place B). For simplicity we are not counting 
with many conditions like operation time on batteries, narrow streets, intersections, job chaining etc.

 Task creation is predictable, new task for each line / process is created exactly after given time after the 
previous job is picked up by AGV at place A.

 Goal is to find best possible strategy for assigning the jobs to AGVs in order to fulfill the job without penalties 
with minimum number of AGVs and with maximum time spent in depo. Penalty is a time when line is not 
able to continue with its takt time due late pickup.

Rules

 • Goal 1: minimize penalty to zero if possible, or to minimum,
 
 • Goal 2: minimize number of used AGVs,
 
 • Goal 3: maximize overall time in depo,
 
 • All AGVs start from depo,
 
 • Available AGVs count is set by parameter,
 
 • TaktTime defines how often there will be a transport order, next Takt is counted from time AGV 
arrives at pickup point and takt time from previous pick up is reached,

 • PenaltyTime is waiting time of transports to be picked up after takt time from previous pick up 
was reached,

 • IdleTime shows how much time all AGVs spent in depo,
 
 • TotalDurationTime is time for how long we want to drive,
 
 • Driving is finished when last AGV returns to depo. Last order is taken if its pickup time is less or 
equal then TotalDurationTime,

 • All streets are bidirectional with same time to travel (A -> B always equals to B -> A) distance 
matrix is defined for 1 direction only.
