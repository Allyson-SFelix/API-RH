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

*/