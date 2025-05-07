/*

CREATE TRIGGER trigger_incrementa
AFTER INSERT ON funcionarios
FOR EACH ROW
EXECUTE FUNCTION incrementar_qtd_funcionarios();


CREATE TRIGGER trigger_decrementa
AFTER DELETE ON funcionarios
FOR EACH ROW
EXECUTE FUNCTION decrementar_qtd_funcionarios();


CREATE TRIGGER trigger_muda_qtd_funcionarios
AFTER UPDATE ON funcionarios
FOR EACH ROW
WHEN (OLD.setor_id IS DISTINCT FROM NEW.setor_id)  apenas quando setor_id antigo for diferente do novo quando houver um UPDATE
EXECUTE FUNCTION mudar_qtd_funcionarios();
*/