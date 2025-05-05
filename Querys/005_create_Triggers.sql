/*

CREATE TRIGGER trigger_incrementa
AFTER INSERT ON funcionarios
FOR EACH ROW
EXECUTE FUNCTION incrementar_qtd_funcionarios();


CREATE TRIGGER trigger_decrementa
AFTER DELETE ON funcionarios
FOR EACH ROW
EXECUTE FUNCTION decrementar_qtd_funcionarios();

*/