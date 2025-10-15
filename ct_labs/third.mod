MODULE SecondScope;
    VAR X : INTEGER;
    EXPORT X;
    BEGIN
        X := 34;
    END SecondScope.

MODULE ThirdLab;
    FROM SecondScope IMPORT Y
    VAR X : INTEGER;
    BEGIN
    X := 33;
    END 
    ThirdLab.