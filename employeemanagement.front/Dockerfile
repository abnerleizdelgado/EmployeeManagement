# Usando a imagem oficial do Node.js
FROM node:16

# Definindo o diretório de trabalho dentro do contêiner
WORKDIR /app

# Copia os arquivos package.json e package-lock.json para o contêiner
COPY package*.json ./

# Remover node_modules e package-lock.json antigos, se existirem
RUN rm -rf node_modules package-lock.json \
    && npm install

# Instala as dependências
RUN npm install

# Copia o restante do projeto para o contêiner
COPY . .

# Expondo a porta padrão do Vite
EXPOSE 5173

# Configura o React para ouvir em 0.0.0.0 (para ser acessado fora do contêiner)
ENV HOST=0.0.0.0

CMD ["npm", "run", "dev", "--", "--host", "0.0.0.0"]
