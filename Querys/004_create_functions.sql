/*
CREATE FUNCTION incrementar_qtd_funcionarios()
RETURNS TRIGGER AS $$
BEGIN
    UPDATE setores 
    SET qtd_funcionarios = qtd_funcionarios + 1 
    WHERE id = NEW.id_setor;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE FUNCTION decrementar_qtd_funcionarios()
RETURNS TRIGGER AS $$
BEGIN
    UPDATE setores
    SET qtd_funcionarios=qtd_funcionarios - 1
    WHERE id=OLD.id_setor;
    RETURN OLD;
END;
$$ LANGUAGE plpgsql;


PERMITE QUE CASO A SUBTRAÇÃO RESULTE EM NEGATIVO FIQUE 0

CREATE FUNCTION mudar_qtd_funcionarios()
RETURNS TRIGGER AS $$
BEGIN
    IF OLD.id_setor IS DISTINCT FROM  NEW.id_setor THEN
        
        UPDATE setores 
        SET qtd_funcionarios = GREATEST(0, qtd_funcionarios - 1)  
        WHERE id = OLD.id_setor;

        UPDATE setores
        SET qtd_funcionarios=qtd_funcionarios + 1
        WHERE id=NEW.id_setor;

    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

*/