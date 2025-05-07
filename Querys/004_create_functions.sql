/*
CREATE FUNCTION incrementar_qtd_funcionarios()
RETURNS TRIGGER AS $$
BEGIN
    UPDATE setores 
    SET qtd_funcionarios = qtd_funcionarios + 1 
    WHERE id = NEW.setor_id;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE FUNCTION decrementar_qtd_funcionarios()
RETURNS TRIGGER AS $$
BEGIN
    UPDATE setores
    SET qtd_funcionarios=qtd_funcionarios - 1
    WHERE id=OLD.setor_id;
    RETURN OLD;
END;
$$ LANGUAGE plpgsql;



CREATE FUNCTION mudar_qtd_funcionarios()
RETURNS TRIGGER AS $$
BEGIN
    IF OLD.setor_id IS DISTINCT FROM  NEW.setor_id THEN
        
        UPDATE setores 
        SET qtd_funcionarios = GREATEST(0, qtd_funcionarios - 1)  PERMITE QUE CASO A SUBTRAÇÃO RESULTE EM NEGATIVO FIQUE 0
        WHERE id = NEW.setor_id;

        UPDATE setores
        SET qtd_funcionarios=qtd_funcionarios + 1
        WHERE id=OLD.setor_id;
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

*/