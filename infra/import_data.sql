COPY master_plan
FROM 'master_plan.csv'
WITH DELIMITER ',' HEADER CSV;;

alter table master_plan
add id serial primary key;
